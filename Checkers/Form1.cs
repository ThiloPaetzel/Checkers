using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        int btnMoveRow;
        int btnMoveCol;
        bool btnToMove;
        bool hasBeenChose;
        Button btnMove;
        bool whiteToplay;
        Bitmap white = new Bitmap(@"F:\05-repertoires-ict-ssd\AUTRES\Checkers\Checkers\Images\White.png");
        Bitmap black = new Bitmap(@"F:\05-repertoires-ict-ssd\AUTRES\Checkers\Checkers\Images\Black.png");
        bool isPair;
        int nbr = 0;
        int counter = 0;
        int playCounter = 1;
        const int NBROWCOLS = 8;//Not touch (Number of cols and rows)
        Button[,] arrButtons = new Button[NBROWCOLS, NBROWCOLS];//Array of all the buttons
        public Form1()
        {
            InitializeComponent();
            InitGame();
        }

        private void InitGame()
        {

            for (int i = 0; i < NBROWCOLS; i++)
            {
                for (int j = 0; j < NBROWCOLS; j++)
                {
                    Button button = new Button();
                    arrButtons[i, j] = button;
                    tableLayoutPanel1.Controls.Add(button,j,i);
                    button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    

                    if (counter == 8 || counter == 16 || counter == 24 ||counter == 32 || counter == 40 || counter == 48 || counter == 56 || counter == 64)
                    {
                        nbr++;
                    }
                    if (nbr % 2 == 1)
                    {
                        isPair = true;
                    }
                    else
                    {
                        isPair = false;
                    }
                    if (isPair == true)
                    {
                        if (counter % 2 == 0)
                        {
                            arrButtons[i, j].BackColor = Color.ForestGreen;
                            arrButtons[i, j].Tag = "Placable";
                        }
                    }
                    if (isPair == false)
                    {
                        if (counter % 2 == 1)
                        {
                            arrButtons[i, j].BackColor = Color.ForestGreen;
                            arrButtons[i, j].Tag = "Placable";
                        }
                    }

                    if (nbr <= 2 && arrButtons[i,j].Tag == "Placable")
                    {
                        arrButtons[i, j].Image = white;
                        arrButtons[i, j].Tag = "White";
                    }

                    if (nbr >= 5 && arrButtons[i,j].Tag == "Placable")
                    {
                        arrButtons[i, j].Image = black;
                        arrButtons[i, j].Tag = "Black";
                    }
                    counter++;
                    arrButtons[i, j].Click += new EventHandler(this.arrButtons_click);
                }
            }
        }

        private void arrButtons_click(object sender, EventArgs e)
        {
            Button button = new Button();
            Button btnClicked = (Button)sender;
            int rowBtnClickedCell = tableLayoutPanel1.GetRow(btnClicked);//Row of the button clicked
            int colBtnClickedCell = tableLayoutPanel1.GetColumn(btnClicked);//Collumn of the button clicked
            if (btnClicked.Tag == "Black" || btnClicked.Tag == "White")
            {
                btnToMove = true;
                btnMove = btnClicked;
                btnMoveRow = tableLayoutPanel1.GetRow(btnClicked);
                btnMoveCol = tableLayoutPanel1.GetColumn(btnClicked);
                hasBeenChose = true;
            }
            if (playCounter % 2 == 0)
            {
                whiteToplay = false;
            }
            else
            {
                whiteToplay = true;
            }

            if (whiteToplay == true)
            {
                if (btnToMove == true && hasBeenChose == true && btnMove.Tag == "White")
                {
                    btnMove.Image = null;
                    btnMove.BackColor = Color.ForestGreen;
                    btnMove.Tag = "Placable";
                }
                else if (btnMove.Tag == "Black")
                {
                    hasBeenChose = false;
                }

                if (btnClicked.Tag == "Placable" && btnToMove == false && hasBeenChose == true)
                {
                    if (btnMoveRow + 1 == rowBtnClickedCell && btnMoveCol + 1 == colBtnClickedCell || btnMoveRow + 1 == rowBtnClickedCell && btnMoveCol - 1 == colBtnClickedCell)
                    {
                        btnClicked.Image = white;
                        btnClicked.Tag = "White";
                        playCounter++;
                        hasBeenChose = false;
                    }

                    //Mange un noir en allant vers la droite
                    else if (btnMoveRow +2 == rowBtnClickedCell && btnMoveCol + 2 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                        }
                    }

                    //Mange un noir en allant vers la gauche
                    else if (btnMoveRow + 2 == rowBtnClickedCell && btnMoveCol - 2 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell - 1, colBtnClickedCell +1].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                        }
                    }
                }
            }

            if (whiteToplay == false)
            {
                if (btnToMove == true && hasBeenChose == true && btnMove.Tag == "Black")
                {
                    btnMove.Image = null;
                    btnMove.BackColor = Color.ForestGreen;
                    btnMove.Tag = "Placable";
                }
                else if (btnMove.Tag == "White")
                {
                    hasBeenChose = false;
                }

                if (btnClicked.Tag == "Placable" && btnToMove == false && hasBeenChose == true)
                {
                    if (btnMoveRow - 1 == rowBtnClickedCell && btnMoveCol - 1 == colBtnClickedCell || btnMoveRow - 1 == rowBtnClickedCell && btnMoveCol + 1 == colBtnClickedCell)
                    {
                        btnClicked.Image = black;
                        btnClicked.Tag = "Black";
                        playCounter++;
                        hasBeenChose = false;
                    }


                    //Gauche
                    else if (btnMoveRow - 2 == rowBtnClickedCell && btnMoveCol - 2 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                        }

                    }
                    //Droite
                    else if (btnMoveRow - 2 == rowBtnClickedCell && btnMoveCol + 2 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                        }


                       
                    }
                }
            }
            btnToMove = false;
        }
    }
}
