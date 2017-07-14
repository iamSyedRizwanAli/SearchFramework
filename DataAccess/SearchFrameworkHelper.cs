using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data;

namespace DataAccess
{
    public static class SearchFrameworkHelper
    {
        private static List<Operators> staticOperatorsList;
        private static List<Columns> staticColumnsList;
        private static List<Pages> staticPagesList;

        public static List<Operators> GetOperators()
        {
            if (staticColumnsList == null)
            {

                List<Operators> operators = new List<Operators>();
                using (DBHelper helper = new DBHelper())
                {
                    DataTable dt = helper.getDataTable("select * from operators");

                    foreach (DataRow row in dt.Rows)
                    {
                        Operators temp = new Operators();
                        temp.OperatorID = (int)row[0];
                        temp.OperatorName = (String)row[1];

                        operators.Add(temp);
                    }

                }
                staticOperatorsList = operators;
            }
            return staticOperatorsList;
        }

        public static List<Columns> GetColumns(int pageId)
        {
            if (staticColumnsList == null)
            {
                List<Columns> columns = new List<Columns>();

                using (DBHelper helper = new DBHelper())
                {
                    DataTable dt = helper.getDataTable("select field_id, display_name, column_name, page_id from columns");

                    if (dt.Rows.Count > 0)
                        foreach (DataRow row in dt.Rows)
                        {
                            Columns temp = new Columns();

                            temp.FieldID = (int)row[0];
                            temp.DisplayName = (String)row[1];
                            temp.ColumnName = (String)row[2];
                            temp.PageID = (int)row[3];

                            columns.Add(temp);
                        }
                }
                staticColumnsList = columns;
            }

            return (from cols in staticColumnsList where cols.PageID == pageId select cols).ToList<Columns>();

        }

        public static Pages GetPage(int pageId)
        {
            if (staticPagesList == null)
                staticPagesList = GetListOfPages();

            Pages[] x = (from p in staticPagesList where p.PageID == pageId select p).ToArray<Pages>();
            
            return x[0];

        }

        public static DataTable FetchAllDataOfPage(Pages page)
        {
            DataTable dt;

            using (DBHelper helper = new DBHelper())
            {
                dt = helper.getDataTable(page.BaseQuery);
            }

            return dt;
        }

        public static DataTable Search(List<SearchParams> sParams)
        {
            if (staticPagesList == null)
                staticPagesList = GetListOfPages();

            if (sParams == null)
                return null;

            if (sParams.Count > 0)
            {
                int colId = sParams.ElementAt(0).FieldID;

                Columns[] colForFindingRunningPage = (from c in staticColumnsList where c.FieldID == colId select c).ToArray<Columns>();

                Pages[] runningPage = (from p in staticPagesList where p.PageID == colForFindingRunningPage[0].PageID select p).ToArray<Pages>();

                String whereClause = PrepareWhereClause(sParams, "");

                String query = runningPage[0].BaseQuery + whereClause;

                DataTable dt;

                using (DBHelper helper = new DBHelper())
                {
                    dt = helper.getDataTable(query);
                }
                return dt;
            }
            else
                return null;
        }

        private static String PrepareWhereClause(List<SearchParams> sParams, String whereClause)
        {
            foreach (SearchParams sp in sParams)
            {
                if (whereClause.Equals(""))
                    whereClause += " where";

                String [] items = new String [4];

                Columns[] currCol = (from c in staticColumnsList where c.FieldID == sp.FieldID select c).ToArray<Columns>();

                items[0] = (sp.ConditionalOperator == null ? "" : sp.ConditionalOperator);
                items[1] = currCol[0].ColumnName;
                items[2] = "LIKE";
                items[3] = sp.Value;

                if (sp.OperatorID == 1)
                    items[3] = "'" + items[3] + "'";
                else if (sp.OperatorID == 2)
                    items[3] = "'" + items[3] + "%'";
                else if (sp.OperatorID == 3)
                    items[3] = "'%" + items[3] + "'";
                else if (sp.OperatorID == 4)
                    items[3] = "'%" + items[3] + "%'";

                String minWhereClause = String.Join(" ", items);
                whereClause += minWhereClause;
            }

            return whereClause;
        }

        private static List<Pages> GetListOfPages()
        {
            List<Pages> tempPages = new List<Pages>();

            String query = "select page_id, page_name, display_name, base_query from pages";

            using (DBHelper helper = new DBHelper())
            {

                DataTable dt = helper.getDataTable(query);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                    {
                        Pages temp = new Pages();

                        temp.PageID = (int)row[0];
                        temp.PageName = (String)row[1];
                        temp.DisplayName = (String)row[2];
                        temp.BaseQuery = (String)row[3];

                        tempPages.Add(temp);
                    }
            }
            return tempPages;
        }

    }

}
