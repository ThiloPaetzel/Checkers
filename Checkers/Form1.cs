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
        bool isKing;
        int numberOfWhite;
        int numberOfBlack;
        int btnMoveRow;
        int btnMoveCol;
        bool btnToMove;
        bool hasBeenChose;
        Button btnMove;
        bool whiteToplay;
        Bitmap white = new Bitmap(@"Images/White.png");
        Bitmap whiteKing = new Bitmap(@"Images/White king.png");
        Bitmap blackKing = new Bitmap(@"Images/Black king.png");
        Bitmap black = new Bitmap(@"Images/Black.png");
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
            lblNbrBlack.Text = "Black : " + Convert.ToString(numberOfBlack);
            lblNbrWhite.Text = "White : " + Convert.ToString(numberOfWhite);
            lblTour.Text = "Tour : White";
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
                        numberOfWhite++;
                    }
                    if (nbr >= 5 && arrButtons[i,j].Tag == "Placable")
                    {
                        arrButtons[i, j].Image = black;
                        arrButtons[i, j].Tag = "Black";
                        numberOfBlack++;
                    }
                    counter++;
                    arrButtons[i, j].Click += new EventHandler(this.arrButtons_click);
                }
            }
        }

        private void arrButtons_click(object sender, EventArgs e)
        {
            
            Button btnClicked = (Button)sender;
            int rowBtnClickedCell = tableLayoutPanel1.GetRow(btnClicked);//Row of the button clicked
            int colBtnClickedCell = tableLayoutPanel1.GetColumn(btnClicked);//Collumn of the button clicked
            if (btnClicked.Tag == "Black" || btnClicked.Tag == "White")
            {
                btnToMove = true;
                btnMove = btnClicked;
                btnMoveRow = tableLayoutPanel1.GetRow(btnClicked);//Row of the button to move
                btnMoveCol = tableLayoutPanel1.GetColumn(btnClicked);//Col of the button to move
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
                    if (btnMove.Image == whiteKing)
                    {
                        isKing = true;
                    }
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
                        lblTour.Text = "Tour : Black";
                        if (rowBtnClickedCell == 7)
                        {
                            btnClicked.Image = whiteKing;
                        }
                    }
                    else if (isKing == true)
                    {
                        btnClicked.Image = whiteKing;
                        btnClicked.Tag = "White";
                        isKing = false;
                        playCounter++;
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
                            numberOfBlack--;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
                        }
                    }

                    //Mange deux noir premier a droite puis gauche ou gauche droite
                    else if (btnMoveRow + 4 == rowBtnClickedCell && btnMoveCol == colBtnClickedCell)
                    {
                        //Droite gauche
                        if (arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Tag == "Black" && arrButtons[rowBtnClickedCell - 3,colBtnClickedCell + 1].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 1].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfBlack -= 2;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
                        }
                        //Gauche droite
                        else if (arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Tag == "Black" && arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 1].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 1].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfBlack -= 2;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
                        }
                    }

                    //Gauche Gauche
                    else if (btnMoveRow + 4 == rowBtnClickedCell && btnMoveCol - 4 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Tag == "Black" && arrButtons[rowBtnClickedCell - 3, colBtnClickedCell + 3].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell + 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell + 3].Image = null;
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell + 3].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfBlack -= 2;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
                        }
                        
                    }

                    //Droite Droite
                    else if (btnMoveRow + 4 == rowBtnClickedCell && btnMoveCol + 4 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell - 1,colBtnClickedCell -1].Tag == "Black" && arrButtons[rowBtnClickedCell - 3,colBtnClickedCell- 3].Tag == "Black")
                        {
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell - 1, colBtnClickedCell - 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 3].Image = null;
                            arrButtons[rowBtnClickedCell - 3, colBtnClickedCell - 3].Tag = "Placable";

                            btnClicked.Image = white;
                            btnClicked.Tag = "White";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfBlack -= 2;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
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
                            numberOfBlack--;
                            lblTour.Text = "Tour : Black";
                            if (rowBtnClickedCell == 7)
                            {
                                btnClicked.Image = whiteKing;
                            }
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
                        lblTour.Text = "Tour : White";
                        if (rowBtnClickedCell == 0)
                        {
                            btnClicked.Image = blackKing;
                        }
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
                            numberOfWhite --;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
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
                            numberOfWhite--;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
                        }
                    }
                    //Droite droite
                    else if (btnMoveRow - 4 == rowBtnClickedCell && btnMoveCol + 4 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Tag == "White" && arrButtons[rowBtnClickedCell + 3,colBtnClickedCell - 3].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell - 3].Image = null;
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell - 3].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfWhite-=2;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
                        }
                    }
                    //Gauche gauche
                    else if (btnMoveRow - 4 == rowBtnClickedCell && btnMoveCol - 4 == colBtnClickedCell)
                    {
                        if (arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Tag == "White" && arrButtons[rowBtnClickedCell + 3, colBtnClickedCell + 3].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell + 3].Image = null;
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell + 3].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfWhite -= 2;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
                        }
                    }
                    // droite gauche gauche droite
                    else if (btnMoveRow - 4 == rowBtnClickedCell && btnMoveCol == colBtnClickedCell)
                    {
                        //droite gauche
                        if (arrButtons[rowBtnClickedCell + 1,colBtnClickedCell + 1].Tag == "White" && arrButtons[rowBtnClickedCell + 3,colBtnClickedCell + 1].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell + 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell + 1].Image = null;
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell + 1].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfWhite -= 2;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
                        }
                        //gauche droite
                        else if (arrButtons[rowBtnClickedCell + 1,colBtnClickedCell - 1].Tag == "White" && arrButtons[rowBtnClickedCell + 3,colBtnClickedCell - 1].Tag == "White")
                        {
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell + 1, colBtnClickedCell - 1].Tag = "Placable";
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell - 1].Image = null;
                            arrButtons[rowBtnClickedCell + 3, colBtnClickedCell - 1].Tag = "Placable";

                            btnClicked.Image = black;
                            btnClicked.Tag = "Black";
                            playCounter++;
                            hasBeenChose = false;
                            numberOfWhite -= 2;
                            lblTour.Text = "Tour : White";
                            if (rowBtnClickedCell == 0)
                            {
                                btnClicked.Image = blackKing;
                            }
                        }
                    }
                }
            }
            btnToMove = false;
            lblNbrBlack.Text = "Black : " + Convert.ToString(numberOfBlack);
            lblNbrWhite.Text = "White : " + Convert.ToString(numberOfWhite);
            if (numberOfWhite == 0)
            {
                DialogResult win = MessageBox.Show("Les noirs ont gagnés", "Partie terminée", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (win == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
            if (numberOfBlack == 0)
            {
                DialogResult win = MessageBox.Show("Les blancs ont gagnés", "Partie terminée", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (win == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }
    }
}
