using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/********************************************************
 * Einstiegsbsp. für einfache, bewegte Grafik.
 * roter Ball (Kreis) gewegt sich  von links nach rechts 
 * über einen Hintergrund
 * letzte Änderung: 21.03.2021
 * ******************************************************/

namespace Ball
{
    public partial class Form1 : Form
    {
        private Bitmap hinterbmp;	//Handle für Hintergrund, der im Konstruktor geladen wird.
	    private Bitmap anzeige;
	    private int x,y;    //Koordinaten des Balls
		private int speed = 15; 

        public Form1()
        {

			InitializeComponent();
			hinterbmp =new Bitmap("wasser.bmp"); //Hintergrundbild laden (liegt in bin\Debug\... bei der exe)
			anzeige =new Bitmap(hinterbmp.Width,hinterbmp.Height);
			//pictureBox1.ClientSize=hinterbmp.Size; // Größe der PictureBox an die Größe des Hintergrunds anpassen
			//pictureBox1.Image=hinterbmp;	//Hintergrund anzeigen lassen
			x=hinterbmp.Width/2;			//Der Ball startet in der Bildmitte
			y=hinterbmp.Height/2;
			DoubleBuffered=true;	//"Flimmerschutz", steht hier nur, damit es nicht vergessen wird.
									// Wird in der Regel mit Designer voreingestellt
		  
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
			if (e.KeyChar == '-')
				speed--;
			else if (e.KeyChar == '+')
				speed++;
			if (speed < 1)
				speed = 30;
			if (speed > 30)
				speed = 1;

			label1.Text = Convert.ToString(speed);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
					// 1. Zunächst stellen wir die aktuelle ansicht auf dem Bitmap anzeige zusammen...
			Graphics  gb=Graphics.FromImage(anzeige);	//Im Bitmap anzeige wird die aktuelle ansicht zusammengesetzt
			gb.DrawImage(hinterbmp,0,0,hinterbmp.Width,hinterbmp.Height); //Bisherige ansicht mit Hintergrund überdecken (löschen)
			
			gb.FillEllipse(Brushes.Red, x, y, 20, 20);	// Unseren Ball als roten Kreis an seine Koordinaten setzen
			gb.Dispose(); //Ressourcen des lokalen Graphics-Objekts wieder feigeben
			
					// 2. Nun lassen wir die aktuelle Ansicht anzeigen
			pictureBox1.Image=anzeige; // Die aktuelle Ansicht auf den Bitmap anzeige in der PictureBox anzeigen lassen
			
					// 3. Abschließen dverändern wir Koordinaten der Darstellung, damit sich auf der folgenden Ansicht etwas "bewegt"
			x = x + 10;	//Koordinaten  des Balls für den nächsten Durchlauf verändern, hier 10 Pixel nach rechts
			
			if (x > anzeige.Width) //verlässt der Ball den rechten Bildrand...
            {
				x = 0;   //...erscheint er wieder von links
			}
			timer1.Interval = speed;
		}
    }
}
/*
 * Experimentiern Sie mit diesem Programm.
 * Verändern Sie den Wert, der bei jedem Durchlauf zur x-Koord. addiert wird....
 * Verändern Sie das Intervall des Timers ...
 * ... Damit haben Sie die beiden Parameter, mit denen sich die Geschwindigkeit der Bewegung innerhalb
 * ... bestimmter Grenzen beeinflussen lässt.
 * Verwenden Sie testweise einen anderen Hintergrund...
 * Die Optik hängt natürlich hier etwas von der Leistungsfähigkeit des Rechners ab.
 * Das Seten von DoubleBuffered auf true muss nicht unbedingt eine Auswirkung haben, 
 * aber Sie werden erkennen, wenn Sie es brauchen ;-)
 */
