using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class _Default : Page
    {
        protected string birth_t = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListView1_OnSorting(object sender, ListViewSortEventArgs e)
        {

        }

        protected void ListView1_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "_Insert")
            {
                TextBox code = (TextBox)e.Item.FindControl("txtcodeIn");
                TextBox name = (TextBox)e.Item.FindControl("txtnameIn");
                TextBox position = (TextBox)e.Item.FindControl("txtpositionIn");
                TextBox department = (TextBox)e.Item.FindControl("txtdepartmentIn");
                TextBox province = (TextBox)e.Item.FindControl("txtprovinceIn");
                TextBox tb_birth = (TextBox)e.Item.FindControl("txtbirthIn");
                //DateTime birth = DateTime.Parse(((TextBox)e.Item.FindControl("txtbirth")).Text);
                if (tb_birth.Text != "")
                {
                    DateTime birth = DateTime.Parse(tb_birth.Text);
                    birth_t = birth.Year.ToString() + '-' + birth.Month.ToString() + '-' + birth.Day.ToString();
                }
                sqlDataSource1.InsertCommand = "sp_memberIn '" + code.Text + "','" + name.Text + "','" + position.Text +
                    "','"+department.Text+"','"+province.Text+"','"+birth_t+"'";
                sqlDataSource1.Insert();
                ListView1.DataBind();

                //Label1.Text = code.Text + name.Text + position.Text + department.Text + province.Text + birth_t;
            }

            if(e.CommandName == "_Cancel")
            {
                ListView1.EditIndex = -1;
                ListView1.DataBind();
            }

            if(e.CommandName == "_Update")
            {
                int _id = int.Parse(e.CommandArgument.ToString());
                TextBox name = (TextBox)e.Item.FindControl("txtname");
                TextBox position = (TextBox)e.Item.FindControl("txtposition");
                TextBox department = (TextBox)e.Item.FindControl("txtdepartment");
                TextBox province = (TextBox)e.Item.FindControl("txtprovince");
                TextBox tb_birth = (TextBox)e.Item.FindControl("txtbirth");

                if(tb_birth.Text != "")
                {
                    DateTime birth = DateTime.Parse(((TextBox)e.Item.FindControl("txtbirth")).Text);
                    birth_t = birth.Year.ToString() +'-'+birth.Month.ToString()+'-'+birth.Day.ToString();
                }

                //Label1.Text = name.Text + position.Text + department.Text + province.Text + t_birth;
                sqlDataSource1.UpdateCommand = "exec sp_memberUp '" + _id + "','" + name.Text + "','" + position.Text +
                    "','" + department.Text + "','" + province.Text + "','" + birth_t + "'";
                sqlDataSource1.Update();
                ListView1.EditIndex = -1;
                ListView1.DataBind();
            }
            if (e.CommandName == "_Delete")
            {
                int _id = int.Parse(e.CommandArgument.ToString());
                sqlDataSource1.DeleteCommand = "exec sp_memberDel '" + _id + "'";
                sqlDataSource1.Delete();
                ListView1.EditIndex = -1;
                ListView1.DataBind();
            }
        }
    }
}