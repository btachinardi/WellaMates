using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WellaMates.Helpers
{
    /// <summary>
    /// HtmlTableParser parses the contents of an html string into a System.Data DataSet or DataTable.
    /// </summary>
    public class HtmlTableParser
    {
        private const RegexOptions ExpressionOptions = RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase;

        private const string CommentPattern = "<!--(.*?)-->";
        private const string TablePattern = "<table[^>]*>(.*?)</table>";
        private const string HeaderPattern = "<th[^>]*>(.*?)</th>";
        private const string RowPattern = "<tr[^>]*>(.*?)</tr>";
        private const string CellPattern = "<td[^>]*>(.*?)</td>";



        /// <summary>
        /// Given an HTML string containing n table tables, parse them into a DataSet containing n DataTables.
        /// </summary>
        /// <param name="html">An HTML string containing n HTML tables</param>
        /// <returns>A DataSet containing a DataTable for each HTML table in the input HTML</returns>
        public static DataSet ParseDataSet(string html)
        {
            DataSet dataSet = new DataSet();
            MatchCollection tableMatches = Regex.Matches(
                WithoutComments(html),
                TablePattern,
                ExpressionOptions);

            foreach (Match tableMatch in tableMatches)
            {
                dataSet.Tables.Add(ParseTable(tableMatch.Value));
            }

            return dataSet;
        }

        /// <summary>
        /// Given an HTML string containing a single table, parse that table to form a DataTable.
        /// </summary>
        /// <param name="tableHtml">An HTML string containing a single HTML table</param>
        /// <returns>A DataTable which matches the input HTML table</returns>
        public static DataTable ParseTable(string tableHtml)
        {
            string tableHtmlWithoutComments = WithoutComments(tableHtml);

            DataTable dataTable = new DataTable();

            MatchCollection rowMatches = Regex.Matches(
                tableHtmlWithoutComments,
                RowPattern,
                ExpressionOptions);

            dataTable.Columns.AddRange(tableHtmlWithoutComments.Contains("<th")
                                           ? ParseColumns(tableHtml)
                                           : GenerateColumns(rowMatches));

            ParseRows(rowMatches, dataTable);

            return dataTable;
        }

        /// <summary>
        /// Strip comments from an HTML stirng
        /// </summary>
        /// <param name="html">An HTML string potentially containing comments</param>
        /// <returns>The input HTML string with comments removed</returns>
        private static string WithoutComments(string html)
        {
            return Regex.Replace(html, CommentPattern, string.Empty, ExpressionOptions);
        }

        /// <summary>
        /// Add a row to the input DataTable for each row match in the input MatchCollection
        /// </summary>
        /// <param name="rowMatches">A collection of all the rows to add to the DataTable</param>
        /// <param name="dataTable">The DataTable to which we add rows</param>
        private static void ParseRows(MatchCollection rowMatches, DataTable dataTable)
        {
            foreach (Match rowMatch in rowMatches)
            {
                // if the row contains header tags don't use it - it is a header not a row
                if (!rowMatch.Value.Contains("<th"))
                {
                    DataRow dataRow = dataTable.NewRow();

                    MatchCollection cellMatches = Regex.Matches(
                        rowMatch.Value,
                        CellPattern,
                        ExpressionOptions);

                    for (int columnIndex = 0; columnIndex < cellMatches.Count; columnIndex++)
                    {
                        dataRow[columnIndex] = cellMatches[columnIndex].Groups[1].ToString();
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
        }

        /// <summary>
        /// Given a string containing an HTML table, parse the header cells to create a set of DataColumns
        /// which define the columns in a DataTable.
        /// </summary>
        /// <param name="tableHtml">An HTML string containing a single HTML table</param>
        /// <returns>A set of DataColumns based on the HTML table header cells</returns>
        private static DataColumn[] ParseColumns(string tableHtml)
        {
            MatchCollection headerMatches = Regex.Matches(
                tableHtml,
                HeaderPattern,
                ExpressionOptions);

            return (from Match headerMatch in headerMatches
                    select new DataColumn(headerMatch.Groups[1].ToString())).ToArray();
        }

        /// <summary>
        /// For tables which do not specify header cells we must generate DataColumns based on the number
        /// of cells in a row (we assume all rows have the same number of cells).
        /// </summary>
        /// <param name="rowMatches">A collection of all the rows in the HTML table we wish to generate columns for</param>
        /// <returns>A set of DataColumns based on the number of celss in the first row of the input HTML table</returns>
        private static DataColumn[] GenerateColumns(MatchCollection rowMatches)
        {
            int columnCount = Regex.Matches(
                rowMatches[0].ToString(),
                CellPattern,
                ExpressionOptions).Count;

            return (from index in Enumerable.Range(0, columnCount)
                    select new DataColumn("Column " + Convert.ToString(index))).ToArray();
        }
    }
}