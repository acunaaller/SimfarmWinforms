using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimFarm
{
    public partial class Form1 : Form
    {

        public delegate void SendUserNameEventHandler(object source, SendUserNameArgs args);

        public event SendUserNameEventHandler UserNameSent;






        /// <summary>
        /// Este metodo esconde todos los paneles menos el que tiene que mostrar
        /// </summary>
        /// <param name="panel">el parametro panel es el nombre del panel que no tiene que esconder</param>
       
        public void Hideallpanelsexcepttheoneshowing(Panel panel)
        {
            LogInViewPanel.Hide();
            ChooseMapPanel.Hide();
            DidPlayerLikedMapPanel.Hide();
            MainMenuPanel.Hide();
            panel.Show();

        }

        public Form1()
        {
            InitializeComponent();
        }


        // este es el codigo para cuando se apreta el boton de nuevo juego y se ingresa el nombre
        private void LogInViewNewGameButton_Click(object sender, EventArgs e)
        {
            //Hideallpanelsexcepttheoneshowing(LogInViewPanel);
            //obtengo el string de la caja de texto y la guardo como userName
            string userName = LogInViewUserNameTextBox.Text;
            LogInViewAcceptedUserWelcomeLabel.Text = "¡Hola " + userName + "!";
            LogInViewAcceptedUserWelcomeLabel.Show();


            OnUserNameSent(userName);


            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(4000);
            ChooseMapPanel.Show();
            LogInViewPanel.Hide();
            //OnUserHasLoggedIn(userName);
            ///-----------------------------------------------

        }

        protected virtual void OnUserNameSent(string userName)
        {
            if (UserNameSent != null)
            {
                UserNameSent(this, new SendUserNameArgs() {userName = userName});
            }
        }

        #region aca estan todos los botones apretar para verlos

        //este codigo es cuando se apreta el boton de cargar juego
        private void LogInViewLoadSavedGameButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(ChooseMapPanel);
            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            ChooseMapPanel.Show();
            LogInViewPanel.Hide();
            //OnUserHasLoggedIn(userName);
            ///-----------------------------------------------
        }

        // este es el codigo para cuando se escoge crear mapa solo de tierra
        private void ChooseMapCreateJustTerrainButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(ChooseMapPanel);
            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            DidPlayerLikedMapPanel.Show();
            ChooseMapPanel.Hide();
            ///-----------------------------------------------
        }
        //este es el codigo para cuando se escoge crear mapa con lago
        private void ChooseMapCreateTerrainWithLakeButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(ChooseMapPanel);
            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            DidPlayerLikedMapPanel.Show();
            ChooseMapPanel.Hide();
            ///-----------------------------------------------
        }
        //este es el codigo para cuando se escoge crear mapa con rio
        private void ChooseMapCreateTerrainWithRiverButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(ChooseMapPanel);

            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            DidPlayerLikedMapPanel.Show();
            ChooseMapPanel.Hide();
            ///-----------------------------------------------
        }
        //este es el codigo para cuando se escoge crear mapa con rio y lago
        private void ChooseMapCreateTerrainWithRiverAndLakeButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(ChooseMapPanel);

            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            DidPlayerLikedMapPanel.Show();
            ChooseMapPanel.Hide();
            ///-----------------------------------------------
        }

        // este codigo es cuando al jugador no le gusto el mapa y se devuelve a preguntarle si le gusta
        private void DidPlayerLikedFarmNoButton_Click(object sender, EventArgs e)
        {
            Hideallpanelsexcepttheoneshowing(DidPlayerLikedMapPanel);

            /// este es el codigo para que cambie de ventanilla
            Thread.Sleep(1000);
            ChooseMapPanel.Show();
            DidPlayerLikedMapPanel.Hide();
            ///-----------------------------------------------
        }
        #endregion

    }
}
