using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace SimFarm.Controllers
{
    public class UserControllerInputs
    {
        static Form1 view;

        public static void OnUserNameSent(object source, SendUserNameArgs e)
        {

        }


        public static void Initializer(Form view)
        {
            UserControllerInputs.view = view as Form1;
            UserControllerInputs.view.UserNameSent += OnUserNameSent;
            return;
        }

        
    }

   
}
