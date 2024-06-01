using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloKnight
{
    public partial class Form1 : Form
    {
        public static List<Image> player = new List<Image>();
        public static List<Image> run = new List<Image>();
        public static List<Image> rrun = new List<Image>();
        public static List<Image> jump = new List<Image>();



        public Form1()
        {
            InitializeComponent();
            #region sprites
            player.Add(Properties.Resources.idle);
            player.Add(Properties.Resources.idle);


            run.Add(Properties.Resources.run1);
            run.Add(Properties.Resources.run2);
            run.Add(Properties.Resources.run3);
            run.Add(Properties.Resources.run4);

            rrun.Add(Properties.Resources.rrun1);
            rrun.Add(Properties.Resources.rrun2);
            rrun.Add(Properties.Resources.rrun3);
            rrun.Add(Properties.Resources.rrun4);

            jump.Add(Properties.Resources.jump1);
            jump.Add(Properties.Resources.jump2);
            jump.Add(Properties.Resources.jump3);
            jump.Add(Properties.Resources.jump4);
            jump.Add(Properties.Resources.land1);
            jump.Add(Properties.Resources.land2);
            jump.Add(Properties.Resources.land3);

            #endregion
            ChangeScreen(this, new MenuScreen());
        }


        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f; // will either be the sender or parent of sender

            if (sender is Form)
            {
                f = (Form)sender;                          //f is sender
            }
            else
            {
                UserControl current = (UserControl)sender;  //create UserControl from sender
                f = current.FindForm();                     //find Form UserControl is on
                f.Controls.Remove(current);                 //remove current UserControl
            }

            // add the new UserControl to the middle of the screen and focus on it
            next.Location = new Point((f.ClientSize.Width - next.Width) / 2,
                (f.ClientSize.Height - next.Height) / 2);
            f.Controls.Add(next);
            next.Focus();
        }
    }
}
