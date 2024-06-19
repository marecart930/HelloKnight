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
        public static List<Image> rjump = new List<Image>();
        public static List<Image> dash = new List<Image>();
        public static List<Image> rdash = new List<Image>();
        public static List<Image> attack = new List<Image>();
        public static List<Image> rattack = new List<Image>();
        public static List<Image> bug = new List<Image>();
        public static List<Image> platform = new List<Image>();


        public Form1()
        {
            InitializeComponent();
            #region sprites
            player.Add(Properties.Resources.idle);
            player.Add(Properties.Resources.idle);
            player.Add(Properties.Resources.idle2);
            player.Add(Properties.Resources.idle2);
            player.Add(Properties.Resources.idle3);
            player.Add(Properties.Resources.idle3);

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

            rjump.Add(Properties.Resources.rjump1);
            rjump.Add(Properties.Resources.rjump2);
            rjump.Add(Properties.Resources.rjump3);
            rjump.Add(Properties.Resources.rjump4);
            rjump.Add(Properties.Resources.rland1);
            rjump.Add(Properties.Resources.rland2);
            rjump.Add(Properties.Resources.rland3);

            dash.Add(Properties.Resources.dash1);
            dash.Add(Properties.Resources.dash2);
            dash.Add(Properties.Resources.dash3);
            dash.Add(Properties.Resources.dash4);
            dash.Add(Properties.Resources.dash5);

            rdash.Add(Properties.Resources.rdash1);
            rdash.Add(Properties.Resources.rdash2);
            rdash.Add(Properties.Resources.rdash3);
            rdash.Add(Properties.Resources.rdash4);
            rdash.Add(Properties.Resources.rdash5);

            attack.Add(Properties.Resources.attack1);
            //attack.Add(Properties.Resources.attack2);
            //attack.Add(Properties.Resources.attack3);

            rattack.Add(Properties.Resources.rattack1);
            //rattack.Add(Properties.Resources.rattack2);
            //rattack.Add(Properties.Resources.rattack3);

            bug.Add(Properties.Resources.bug1);
            bug.Add(Properties.Resources.bug2);
            bug.Add(Properties.Resources.bug3);
            bug.Add(Properties.Resources.bug4);
            bug.Add(Properties.Resources.bug5);
            bug.Add(Properties.Resources.bug6);
            bug.Add(Properties.Resources.bug7);
            bug.Add(Properties.Resources.bug8);
            bug.Add(Properties.Resources.bug9);

            platform.Add(Properties.Resources.platform);


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
