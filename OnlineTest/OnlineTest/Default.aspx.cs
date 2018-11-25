using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace OnlineTest
{
    public partial class Default : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        string cn = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from tblQuestions", cn);
            sda.Fill(dt);
            grdquestions.DataSource = dt;
            grdquestions.DataBind();
        }

        protected void grdquestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList rdbOptions =(RadioButtonList) e.Row.FindControl("rdlstOptions");
                HiddenField hdnQuestionID = (HiddenField)e.Row.FindControl("hdnQuestoinID");
                if (rdbOptions != null && hdnQuestionID != null)
                {
                    DataRow[] result = dt.Select("questionID=" + Convert.ToInt32(hdnQuestionID.Value));
                    DataView view = new DataView();
                    view.Table = dt;
                    view.RowFilter = "questionID=" + Convert.ToInt32(hdnQuestionID.Value);
                    if (view.Table.Rows.Count > 0)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = view.ToTable();
                    }
                }
            }
        }
    }
}