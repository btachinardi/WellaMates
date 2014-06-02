using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TwitterBootstrap3;
using TwitterBootstrapMVC.Controls;

namespace WellaMates.Helpers
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString TableHeaderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return TableHeaderFor(htmlHelper, null, null, null, expression, null);
        }

        public static MvcHtmlString TableHeaderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string currentFilter, string currentSort, Expression<Func<TModel, TProperty>> expression)
        {
            return TableHeaderFor(htmlHelper, null, currentFilter, currentSort, expression, null);
        }

        public static MvcHtmlString TableHeaderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string currentFilter, string currentSort, Expression<Func<TModel, TProperty>> expression)
        {
            return TableHeaderFor(htmlHelper, actionName, currentFilter, currentSort, expression, null);
        }

        public static MvcHtmlString TableHeaderFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string currentFilter, string currentSort, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            return TableHeaderHelper(htmlHelper,
                                  ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData),
                                  ExpressionHelper.GetExpressionText(expression),
                                  actionName,
                                  currentFilter,
                                  currentSort,
                                  HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString TableHeaderHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, string actionName, string currentFilter, string currentSort, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Common_NullOrEmpty", "name");
            }

            var propertyName = metadata.PropertyName;
            var oldSort = currentSort ?? (string)htmlHelper.ViewContext.Controller.ViewBag.CurrentSort;
            currentSort = oldSort != null && oldSort.Contains("Asc") ? propertyName + " Desc" : propertyName + " Asc";
            var routeValues = new RouteValueDictionary(new { @sortOrder = currentSort, @currentFilter = currentFilter ?? (string)htmlHelper.ViewContext.Controller.ViewBag.CurrentFilter });

            return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, metadata.DisplayName, null, actionName ?? htmlHelper.ViewContext.RouteData.Values["action"].ToString(), null, routeValues, htmlAttributes));
        }

        public static MvcHtmlString FakeFileFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return FakeFileHelper(html, ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(html.ViewData)),
                ExpressionHelper.GetExpressionText(expression), htmlAttributes);
        }

        public static MvcHtmlString FakeFileHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, object htmlAttributes)
        {
            var fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
            var fullHtmlFieldId = TagBuilder.CreateSanitizedId(fullHtmlFieldName);

            var global = new TagBuilder("div");
            global.AddCssClass("form-group");

            var inputWrapper = new TagBuilder("div");

            var inputReal = new TagBuilder("input");
            inputReal.MergeAttribute("type", "File");
            inputReal.MergeAttribute("name", fullHtmlFieldName);
            inputReal.MergeAttribute("id", fullHtmlFieldId);
            inputReal.MergeAttribute("style", "opacity:0;height:0;width:0");
            inputReal.MergeAttribute("value", "");

            var inputFakeWrapper = new TagBuilder("div");
            inputFakeWrapper.AddCssClass("fake-file");
            inputFakeWrapper.AddCssClass("not-initialized");
            inputFakeWrapper.AddCssClass("clearfix");
            inputFakeWrapper.MergeAttribute("data-file-target", fullHtmlFieldId);
            inputFakeWrapper.MergeAttribute("style", "padding-left: 15px;");

            var formControlWrapper = new TagBuilder("div");
            formControlWrapper.AddCssClass("static-style");
            formControlWrapper.AddCssClass("input-style");
            formControlWrapper.AddCssClass("form-group");
            formControlWrapper.AddCssClass("col-xs-12");
            formControlWrapper.AddCssClass("col-sm-12");
            formControlWrapper.AddCssClass("col-md-5");
            formControlWrapper.AddCssClass("col-lg-8");
            formControlWrapper.MergeAttribute("style", "width:auto");

            var formControl = new TagBuilder("input");
            formControl.AddCssClass("form-control");
            formControl.MergeAttribute("disabled", "disabled");
            formControl.MergeAttribute("data-val", "true");
            formControl.MergeAttribute("id", "Fake_" + fullHtmlFieldId);
            formControl.MergeAttribute("name", "Fake_" + fullHtmlFieldName);
            formControl.MergeAttribute("type", "text");
            formControl.MergeAttribute("value", "");
            formControlWrapper.InnerHtml = formControl.ToString(TagRenderMode.SelfClosing);

            var searchButtonWrapper = new TagBuilder("div");
            searchButtonWrapper.AddCssClass("col-xs-8");
            searchButtonWrapper.AddCssClass("col-xs-push-4");
            searchButtonWrapper.AddCssClass("col-md-push-0");
            searchButtonWrapper.AddCssClass("col-md-7");
            searchButtonWrapper.AddCssClass("col-lg-4");
            searchButtonWrapper.MergeAttribute("style", "min-width:230px");

            var searchButton = new TagBuilder("a");
            searchButton.AddCssClass("btn");
            searchButton.AddCssClass("btn-lg");
            searchButton.AddCssClass("btn-default");
            searchButton.MergeAttribute("title", "Procurar");
            searchButton.InnerHtml = "Procurar";

            var helpWrapper = new TagBuilder("div");
            helpWrapper.MergeAttribute("style", "height=100%;display:inline");

            var iconHelp = new TagBuilder("span");
            iconHelp.AddCssClass("icon-help");
            iconHelp.InnerHtml = "?";

            var tooltipHelp = new TagBuilder("div");
            tooltipHelp.AddCssClass("tooltip-help");

            var tooltipInner = new TagBuilder("div");
            tooltipInner.AddCssClass("inner-tooltip");

            var tooltipText = new TagBuilder("p")
            {
                InnerHtml =
                    "Tipos de arquivos suportados: png, jpg e gif."
            };
            tooltipInner.InnerHtml = tooltipText.ToString(TagRenderMode.Normal);
            tooltipHelp.InnerHtml = tooltipInner.ToString(TagRenderMode.Normal);

            helpWrapper.InnerHtml = String.Format("{0} {1}", iconHelp.ToString(TagRenderMode.Normal),
                tooltipHelp.ToString(TagRenderMode.Normal));

            searchButtonWrapper.InnerHtml = String.Format("{0} {1}", searchButton.ToString(TagRenderMode.Normal), 
                helpWrapper.ToString(TagRenderMode.Normal));

            inputFakeWrapper.InnerHtml = String.Format("{1} {0}", formControlWrapper.ToString(TagRenderMode.Normal),
                searchButtonWrapper.ToString(TagRenderMode.Normal));


            inputWrapper.InnerHtml = String.Format("{0} {1}", inputReal.ToString(TagRenderMode.Normal),
                inputFakeWrapper.ToString(TagRenderMode.Normal));

            global.InnerHtml = inputWrapper.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(global.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString LeftCheckboxFor<TModel, TValue>(this HtmlHelper html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return CheckboxHelper(html, ModelMetadata.FromLambdaExpression(expression, (ViewDataDictionary<TModel>) html.ViewData),
                ExpressionHelper.GetExpressionText(expression), htmlAttributes);
        }

        internal static MvcHtmlString CheckboxHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName,
            object htmlAttributes)
        {
            var labelText = metadata.GetDisplayName() ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
            var htmlValue = metadata.Model;

            var checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.Attributes.Add("name", fullHtmlFieldName);
            checkbox.Attributes.Add("value", "true");
            checkbox.GenerateId(fullHtmlFieldName);

            if (htmlValue != null && (bool)htmlValue)
                checkbox.Attributes.Add("checked", "checked");

            var hidden = new TagBuilder("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("value", "false");

            var label = new TagBuilder("label");
            label.Attributes.Add("for",
                TagBuilder.CreateSanitizedId(fullHtmlFieldName));
            label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
            label.InnerHtml = String.Format("{0} {1} {2}", checkbox.ToString(TagRenderMode.StartTag),
                hidden.ToString(TagRenderMode.StartTag), labelText);
            return MvcHtmlString.Create(label.ToString(TagRenderMode.Normal));
        }


    }
}