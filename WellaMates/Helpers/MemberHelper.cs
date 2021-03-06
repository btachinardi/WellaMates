﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
using WellaMates.DAL;
using WellaMates.Mailers;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.Helpers
{
    public class MemberHelper
    {
        public static UserProfile GetUserProfile(PortalContext db)
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var membershipUser = Membership.GetUser();
            if (membershipUser == null) throw new InvalidCredentialException("No user currently logged in.");
            var userID = membership.GetUserId(membershipUser.UserName);
            return db.UserProfiles.First(u => u.UserID == userID);
        }

        public static T SetBaseMemberVM<T>(T vm, UserProfile user, string userVmMode = "Salvar") where T : BaseMemberVM
        {
            if (user.RefundProfile != null)
            {
                vm.IsFreelancer = user.RefundProfile.Freelancer != null;
                vm.IsManager = user.RefundProfile.Manager != null;
                vm.IsRefundAdmin = user.RefundProfile.RefundAdministrator != null;
                vm.IsRefundVisualizator = user.RefundProfile.RefundVisualizator != null;
                vm.IsMultiRoles = ((vm.IsFreelancer ? 1 : 0) + (vm.IsManager ? 1 : 0) + (vm.IsRefundAdmin ? 1 : 0) + (vm.IsRefundVisualizator ? 1 : 0)) > 1;
            }
            vm.User = new UserVM().Start(user, EditUserMode.INFO, userVmMode);
            return vm;
        }

        public static BaseMemberVM SetBaseMemberVM(UserProfile user, string userVmMode = "Salvar")
        {
            return new BaseMemberVM
            {
                User = new UserVM().Start(user, EditUserMode.INFO, userVmMode)
            };
        }

        public static string GetCurrentController(PortalContext db)
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var membershipUser = Membership.GetUser();
            if (membershipUser == null)
            {
                return null;
            }
            var cpf = membershipUser.UserName;
            if (Roles.IsUserInRole(cpf, "Admin"))
            {
                return "Admin";
            }
            if (Roles.IsUserInRole(cpf, "Member"))
            {
                if (Roles.IsUserInRole(cpf, "RefundVisualizator"))
                {
                    return "RefundVisualizator";
                }
                if (Roles.IsUserInRole(cpf, "RefundAdministrator"))
                {
                    return "RefundAdministrator";
                }
                if (Roles.IsUserInRole(cpf, "Manager"))
                {
                    return "Manager";
                }

                if (Roles.IsUserInRole(cpf, "Freelancer"))
                {
                    return "Freelancer";
                }
            }
            throw new InvalidDataException("Could not resolve current user's controller");
        }
        /**
        public static bool GenerateUpdates(PortalContext db, Refund refund, RefundProfile user, Refund oldRefund)
        {
            //First case scenario, a whole new refund entity
            if (oldRefund == null)
            {
                db.Refunds.Add(refund);
                foreach (var refundItem in refund.RefundItems)
                {
                    refundItem.Status = RefundItemStatus.CREATED;
                    db.RefundItemUpdates.Add(new RefundItemUpdate
                    {
                        Date = DateTime.Now,
                        RefundItem = refundItem,
                        RefundProfile = user,
                        Status = RefundItemStatus.CREATED
                    });
                }
                oldRefund = refund;
            }
            else
            {
                if (oldRefund.RefundItems == null)
                {
                    oldRefund.RefundItems = new Collection<RefundItem>();
                }
                if (refund.RefundItems == null)
                {
                    refund.RefundItems = new Collection<RefundItem>();
                }
                //Deleted items (don't issue updates for formerly deleted items)
                var deleted =
                    oldRefund.RefundItems.Where(
                        ori =>
                            ori.Status != RefundItemStatus.DELETED &&
                            refund.RefundItems.All(ri => ri.RefundItemID != ori.RefundItemID));
                var deletedList = deleted as IList<RefundItem> ?? deleted.ToList();
                foreach (var item in deletedList)
                {
                    item.Status = RefundItemStatus.DELETED;
                    db.RefundItemUpdates.Add(new RefundItemUpdate
                    {
                        Date = DateTime.Now,
                        RefundItem = item,
                        RefundProfile = user,
                        Status = RefundItemStatus.DELETED
                    });
                }

                foreach (var refundItem in refund.RefundItems)
                {
                    var oldRefundItem =
                        oldRefund.RefundItems.FirstOrDefault(ori => ori.RefundItemID == refundItem.RefundItemID);

                    //New Item Created
                    if (oldRefundItem == null)
                    {
                        refundItem.Status = RefundItemStatus.CREATED;
                        db.RefundItemUpdates.Add(new RefundItemUpdate
                        {
                            Date = DateTime.Now,
                            RefundItem = refundItem,
                            RefundProfile = user,
                            Status = RefundItemStatus.CREATED
                        });
                    }
                    //Already existing item
                    else
                    {
                        //Checks for status changes
                        if (oldRefundItem.Status != refundItem.Status)
                        {
                            oldRefundItem.Status = refundItem.Status;
                            db.RefundItemUpdates.Add(new RefundItemUpdate
                            {
                                Date = DateTime.Now,
                                RefundItem = oldRefundItem,
                                RefundProfile = user,
                                Status = refundItem.Status
                            });
                            db.RefundItems.Attach(oldRefundItem);
                        }
                        //And finally checks if the item was edited
                        else
                        {
                            var changed = false;
                            if (oldRefundItem.Activity != refundItem.Activity)
                            {
                                changed = true;
                                oldRefundItem.Activity = refundItem.Activity;
                            }

                            if (oldRefundItem.Category != refundItem.Category)
                            {
                                changed = true;
                                oldRefundItem.Category = refundItem.Category;
                            }

                            if (oldRefundItem.OtherSpecification != refundItem.OtherSpecification)
                            {
                                changed = true;
                                oldRefundItem.OtherSpecification = refundItem.OtherSpecification;
                            }

                            if (oldRefundItem.Value != refundItem.Value)
                            {
                                changed = true;
                                oldRefundItem.Value = refundItem.Value;
                            }

                            var item = refundItem;
                            if (item.Files != null)
                            {
                                var newFiles = oldRefundItem.Files == null
                                    ? item.Files.ToList()
                                    : item.Files.Where(f => oldRefundItem.Files.All(of => of.FileID != f.FileID));
                                foreach (var newFile in newFiles)
                                {
                                    changed = true;
                                    oldRefundItem.Files.Add(newFile);
                                }
                            }

                            if (oldRefundItem.Files != null)
                            {
                                var deletedFiles = item.Files == null
                                    ? oldRefundItem.Files.ToList()
                                    : oldRefundItem.Files.Where(f => item.Files.All(of => of.FileID != f.FileID)).ToList();
                                foreach (var deletedFile in deletedFiles)
                                {
                                    changed = true;
                                    oldRefundItem.Files.Remove(deletedFile);
                                }
                            }


                            if (changed)
                            {
                                db.RefundItemUpdates.Add(new RefundItemUpdate
                                {
                                    Date = DateTime.Now,
                                    RefundItem = oldRefundItem,
                                    RefundProfile = user,
                                    Status = RefundItemStatus.UPDATED
                                });
                            }
                        }
                    }
                }
            }
            oldRefund.Update();
            db.SaveChanges();
            return true;
        }
         */

        public static bool SendResponse(PortalContext db, RefundProfile refundProfile, RefundItemUpdate[] updates)
        {
            Refund refund = null;
            foreach (var update in updates)
            {
                var refundItem = db.RefundItems.First(r => r.RefundItemID == update.RefundItemID);
                if (refund == null)
                {
                    refund = refundItem.Refund;
                }
                refundItem.Status = update.Status;
                refundItem.ReceivedInvoice = update.ReceivedInvoice;
                db.RefundItemUpdates.Add(new RefundItemUpdate
                {
                    Date = DateTime.Now,
                    Comment = update.Comment,
                    RefundItem = refundItem,
                    RefundProfile = refundProfile,
                    Status = refundItem.Status,
                    Files = update.Files,
                    ReceivedInvoice = update.ReceivedInvoice
                });
            }

            if (refund != null)
            {
                refund.Update();
                db.Refunds.Attach(refund);
                db.Entry(refund).State = EntityState.Modified;
            }
            db.SaveChanges();
            return true;
        }

        public static Freelancer GetRefundFreelancer(Refund refund, PortalContext db)
        {
            var refundOwner = GetRefundOwner(refund, db);

            return refundOwner == null ? null : refundOwner.Freelancer;
        }

        public static IRefundOwner GetRefundOwner(Refund refund, PortalContext db)
        {
            var refundId = refund.RefundID;

            var visit = db.Visits.FirstOrDefault(v => v.RefundID == refundId);
            if (visit != null)
                return visit;

            var evnt = db.Events.FirstOrDefault(e => e.RefundID == refundId);
            if (evnt != null)
                return evnt;

            var monthly = db.Monthlies.FirstOrDefault(m => m.RefundID == refundId);
            if (monthly != null)
                return monthly;

            return null;
        }
    }
}