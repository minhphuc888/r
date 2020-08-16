using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication5.Helper;
using WindowsFormsApplication5.Model;

namespace WindowsFormsApplication5
{
    public partial class FormMain : Form
    {
        private bool bEBL1 = false, bEBL2 = false, bEBL3 = true, bEBL4 = true, NGUON = false;
        private bool HLF = false;
        private int iDrawEllipse = 60, iDrawLine1 = 90, iDrawLine2 = 40;
        private int iDrawEllipse2 = 100, n = -135, n2 = -140;
        private int iMoverButton = 410, iCountThread; //vi trí button

        private Point _centerPoint;
        private Point currentPoint = new Point();
        private Point ToaDoChuotKhiAnButton = new Point();

        Timer t = new Timer();
        int u, number, number2;
        Pen penGreen, penRed;
        Graphics g;
        Bitmap bmp;

        Point ToaDoChuotVeVungBaoDong1;
        Point ToaDoChuotVeVungBaoDong2;

        private bool isArcDrawer = false;
        private List<ArcDrawerModel> arcDrawers;
        private ObjMoveModel objMove;

        public FormMain()
        {
            InitializeComponent();
            InitializeValue();
            setHinhTron();
            if (NGUON)
                this.BackColor = Color.Black;
            u = 0;
            t.Interval = 30;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
            control1 hc = new control1();
            Class1.showcontrol(hc, panelchinh);
        }
        private void InitializeValue()
        {
            _centerPoint = new Point(pboxRadar.Width / 2, pboxRadar.Height / 2);
            arcDrawers = new List<ArcDrawerModel>();
            _drawType = DrawType.TrangThaiNgung;
            pnlObjTest.Location = Point.Empty;
        }

        // phần a mới thêm 23/02
        private void setHinhTron()
        {
            bmp = new Bitmap(pboxRadar.Width, pboxRadar.Height);
            g = Graphics.FromImage(bmp);
            g.TranslateTransform(-1, -1);
            g.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pboxRadar.Width + 1, pboxRadar.Height + 1);
            Region region = new Region(path);
            g.SetClip(region, CombineMode.Replace);
        }
        // hien thi thoi gian

        private void t_Tick(object sender, EventArgs e)
        {
            penGreen = new Pen(Color.LawnGreen, 1f);
            penRed = new Pen(Color.Red, 1f);
            //nut nguon bat
            g.Clear(Color.FromArgb(iSetContr, Color.Gray));

            ChonTrangThai();
            g.DrawClosedCurve();

            foreach (var arcDrawer in arcDrawers)
            {
                g.ArcDrawer(arcDrawer);
            }

            if (objMove != null)
            {
                    g.ObjMoveDrawer(objMove,pnlObjTest);
            }
            //hien do EBL1,2

            // HIEN HLF

            if (iCountThread >= 400)
            {
                if (u % 2 == 0) ButtonMove();
            }

            iCountThread++;
            pboxRadar.Image = bmp;
            penGreen.Dispose();

            u++;
            if (u == 360) u = 0; 
        }
        #region Method

        private void VeCacVongTron()
        {
            int iSizeDrawArc = 100;

            if (NGUON)
                for (int i = 1; i < 3; i++)
                {
                    g.DrawEllipse(penGreen, iSizeDrawArc * i, iSizeDrawArc * i, pboxRadar.Width - iSizeDrawArc * i * 2, pboxRadar.Height - iSizeDrawArc * i * 2);
                    g.DrawEllipse(penGreen, 1, 1, pboxRadar.Width - 1, pboxRadar.Height - 1);

                }
        }

        private void VeVongTronCoTheThayDoi()
        {
            // vẽ 2 vòng tròn
            if (bEBL1)
            {
                if (iDrawEllipse >= 0 && iDrawEllipse <= 280)
                {
                    g.DrawEllipse(penRed, iDrawEllipse, iDrawEllipse, pboxRadar.Width - iDrawEllipse * 2, pboxRadar.Height - iDrawEllipse * 2);
                }

            }
            if (bEBL2)
            {
                if (iDrawEllipse2 >= 0 && iDrawEllipse2 <= 280)
                {
                    g.DrawEllipse(penRed, iDrawEllipse2, iDrawEllipse2, pboxRadar.Width - iDrawEllipse2 * 2, pboxRadar.Height - iDrawEllipse2 * 2);
                }
            }
        }
        private void VeVongTronCoTheThayDoiTH1(Point ToaDoChuot)
        {
            // vẽ 2 vòng tròn
            if (bEBL1)
            {
                if (iDrawEllipse >= 0 && iDrawEllipse <= 280)
                {
                    int iDrawEllipseNew = 325 - iDrawEllipse;
                    Rectangle rect = new Rectangle() { Width = iDrawEllipseNew, Height = iDrawEllipseNew, X = ToaDoChuot.X - (iDrawEllipseNew / 2), Y = ToaDoChuot.Y - (iDrawEllipseNew / 2) };
                    g.DrawArc(penRed, rect, 0, 360);
                }
            }
            if (bEBL2)
            {
                if (iDrawEllipse2 >= 0 && iDrawEllipse2 <= 280)
                {
                    int iDrawEllipseNew = 325 - iDrawEllipse2;
                    Rectangle rect = new Rectangle() { Width = iDrawEllipseNew, Height = iDrawEllipseNew, X = ToaDoChuot.X - (iDrawEllipseNew / 2), Y = ToaDoChuot.Y - (iDrawEllipseNew / 2) };
                    g.DrawArc(penRed, rect, 0, 360);
                }
            }
        }

        private void VeDauCong(Point ToaDoChuot)
        {

        }

        private void VeDuongLineCoTheThayDoi(Point pTamHinhTron)
        {
            if (NGUON == true)
            {
                //vẽ 2 đường line
                if (bEBL3)
                {
                    for (int i = 0; i <= 12; i++)
                    {
                        g.DrawLine(penGreen, pTamHinhTron, DrawHelper.getPointDrawLine(iDrawLine1 + i * 30, _centerPoint, pboxRadar.Width / 2));
                    }
                }
            }
        }

        // kiểm tra xem toa độ chuot co nam ngoai dung tron hay không 
        double iR, iX, iY;
        private bool CheckDrawEllipse(Point pValue)
        {
            if (pValue.X < _centerPoint.X) iX = _centerPoint.X - pValue.X;
            else iX = pValue.X - _centerPoint.X - 20;
            if (pValue.Y < _centerPoint.Y) iY = _centerPoint.Y - pValue.Y;
            else iY = pValue.Y - _centerPoint.Y;
            iR = (double)Math.Sqrt(iX * iX + iY * iY);
            if (iR <= 322)  //  là bán kính của vòng tròn, 
            {
                return true;
            }
            return false;
        }

        // hien do cung
        private void ChonTrangThai()
        {
            if (iR <= 325)
            {
                lbDoCung.Text = DrawHelper.GetAngle(_centerPoint, currentPoint).ToString();
                lbBanKinh.Text = ((int)iR * 10 / 21).ToString();
                if (lbDoCung.Text == "360")
                    lbDoCung.Text = "0";
            }
            if (bTH1)   // nếu được ấn thì chuyển sang trạng thái 
            {
                TrangThaiMot();
            }
            else TrangThaiBinhThuong();
        }

        private void TrangThaiBinhThuong()
        {
            VeCacVongTron();
            VeVongTronCoTheThayDoi();
            VeDuongLineCoTheThayDoi(_centerPoint);
            if (NGUON == true)
            {
                // Đường quét
                for (int i = 0; i < 30; i++)
                {
                    g.DrawLine(penGreen, _centerPoint, DrawHelper.getPointDrawLine(u + i, _centerPoint, pboxRadar.Width / 2));
                }
            }
            if (bTH2)
            {
                TrangThaiDacBiet();
            }
        }

        private void TrangThaiMot()
        {
            if (bClickMouse)
            {
                VeDauCong(ToaDoChuotKhiAnButton);
                VeDuongLineCoTheThayDoi(ToaDoChuotKhiAnButton);
                VeVongTronCoTheThayDoiTH1(ToaDoChuotKhiAnButton);
            }
            if (bTH2)
            {
                TrangThaiDacBiet();
            }
        }

        private void TrangThaiDacBiet()
        {
            if (bClickMouse)
            {
                VeDauCong(ToaDoChuotKhiAnButton);
                g.DrawLine(new Pen(Color.White, 2f), ToaDoChuotKhiAnButton, DrawHelper.getPointDrawLine(iDrawLineDacBietTangGiam, ToaDoChuotKhiAnButton, iDrawLineDacBiet));
            }
        }
        #endregion

        // button di
        private void ButtonMove()
        {
            if (NGUON == true)
            {
                pnlMoverFirst.Visible = true;
                pnlMoverFirst.Location = new Point(iMoverButton, 300);

                DICHUYEN1.Location = new Point(iMoverButton, 300);

                pnlMoverSecond.Visible = true;
                pnlMoverSecond.Location = new Point(400, iMoverButton - 350);

                DICHUYEN2.Location = new Point(400, iMoverButton - 350);

                dichuyen33.Visible = true;

                dichuyen33.Location = new Point(iMoverButton + 100, 350);

                DICHUYEN3.Location = new Point(iMoverButton + 100, 350);

                TH11.Location = new Point(iMoverButton + 50, 400);
                iMoverButton++;
                if (iMoverButton >= 500)
                {
                    iMoverButton = 400;
                }
            }
        }

        #region Events

        private void button3_Click(object sender, EventArgs e)
        {
            if (NGUON == true)
                bEBL1 = !bEBL1;
        }

        private void EBL2_Click(object sender, EventArgs e)
        {
            bEBL2 = !bEBL2;
        }

        private void GIAM_Click(object sender, EventArgs e)
        {

            if (bEBL1 == true && bEBL2 == false)
            {
                iDrawEllipse += 2;
                n--;
                number = n + 230;
            }
            else
            {
                iDrawEllipse2 += 2;
                n2--;
                number2 = n2 + 230;
            }
        }

        private void TANG_Click(object sender, EventArgs e)
        {
            if (bEBL1 == true && bEBL2 == false)
            {
                iDrawEllipse -= 2;
                n++;
                number = n + 230;
            }
            else
            {
                iDrawEllipse2 -= 2;
                n2++;
                number2 = n2 + 230;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            bEBL3 = !bEBL3;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bEBL4 = !bEBL4;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (bEBL3 == true && bEBL4 == false)
            {
                iDrawLine1 -= 1;
            }
            else
            {
                iDrawLine2 -= 1;
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (bEBL3 == true && bEBL4 == false)
            {
                iDrawLine1 += 1;
            }
            else
            {
                iDrawLine2 += 1;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            NGUON = !NGUON;
            if (NGUON == true)
            {
                pnlMoverFirst.Visible = true;
                pnlMoverSecond.Visible = true;
                pnlMoverThird.Visible = true;

                pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\7.pNg");
            }
            else
            {
                dichuyen33.Visible = false;
                pnlMoverFirst.Visible = false;
                pnlMoverSecond.Visible = false;
                pnlMoverThird.Visible = false;

                pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\110.pNg");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NGUON = !NGUON;
            if (NGUON == true)
            {
                pnlMoverFirst.Visible = true;
                pnlMoverSecond.Visible = true;
                pnlMoverThird.Visible = true;
                do00.Visible = true;
                do30.Visible = true;
                do60.Visible = true;
                do90.Visible = true;
                do120.Visible = true;
                do150.Visible = true;
                do180.Visible = true;
                do210.Visible = true;
                do240.Visible = true;
                do270.Visible = true;
                do300.Visible = true;
                do330.Visible = true;
                culytren.Visible = true;
            }
            else
            {
                dichuyen33.Visible = false;
                pnlMoverFirst.Visible = false;
                pnlMoverSecond.Visible = false;
                pnlMoverThird.Visible = false;

                do00.Visible = false;
                do30.Visible = false;
                do60.Visible = false;
                do90.Visible = false;
                do120.Visible = false;
                do150.Visible = false;
                do180.Visible = false;
                do210.Visible = false;
                do240.Visible = false;
                do270.Visible = false;
                do300.Visible = false;
                do330.Visible = false;
                culytren.Visible = false;
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (bEBL3 == true && bEBL4 == false)
            {
                iDrawLine1 -= 1;
            }
            else
            {
                iDrawLine2 -= 1;
            }
            if (bTH2)
                iDrawLineDacBietTangGiam--;
        }
        private int a1 = 40;
        private void button18_Click(object sender, EventArgs e)
        {
            if (bEBL3 == true && bEBL4 == false)
            {
                iDrawLine1 += 1;
            }
            else
            {
                iDrawLine2 += 1;
            }
            if (bTH2)
            {
                a1++;
                iDrawLineDacBietTangGiam++;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (bEBL1 == true && bEBL2 == false)
            {
                iDrawEllipse += 2;
                n--;
                number = n + 230;

            }
            else
            {
                iDrawEllipse2 += 2;
                n2--;
                number2 = n2 + 230;


            }
            if (bTH2)
                iDrawLineDacBiet -= 10;
        }
        private void button19_Click(object sender, EventArgs e)
        {
            if (bEBL1 == true && bEBL2 == false)
            {
                iDrawEllipse -= 2;
                n++;
                number = n + 230;
            }
            else
            {
                iDrawEllipse2 -= 2;
                n2++;
                number2 = n2 + 230;
            }
            if (bTH2)
                iDrawLineDacBiet += 10;
        }
        //F4
        private void PF44_Click(object sender, EventArgs e)
        {
            HLF = !HLF;
        }
        //HLF OFFSET
        private void button22_Click(object sender, EventArgs e)
        {
            HLF = !HLF;
        }
        //HLF OFFSET
        private void button28_Click(object sender, EventArgs e)
        {
            HLF = !HLF;
        }
        // MUC TIEU PANEL DI CHUYEN

        private void pnlMoverFirst_Click(object sender, EventArgs e)
        {
            DICHUYEN1.Visible = true;
            pnlMoverFirst.Visible = false;
        }
        private void DICHUYEN1_Click(object sender, EventArgs e)
        {
            pnlMoverFirst.Visible = true;
            DICHUYEN1.Visible = false;
        }

        private void pnlMoverSecond_Click(object sender, EventArgs e)
        {
            DICHUYEN2.Visible = true;
            pnlMoverSecond.Visible = false;
        }
        private void DICHUYEN2_Click(object sender, EventArgs e)
        {
            pnlMoverSecond.Visible = true;
            DICHUYEN2.Visible = false;
        }
        private void DICHUYEN3_Click(object sender, EventArgs e)
        {
            dichuyen33.Visible = true;
            DICHUYEN3.Visible = false;
        }
        private void dichuyen33_click(object sender, EventArgs e)
        {
            DICHUYEN3.Visible = true;
            dichuyen33.Visible = false;
        }

        //BRILL
        private int iSetContr = 100;
        private int R = 0;
        private void TAGRAIN_Click(object sender, EventArgs e)
        {
            if (R <= 100) R--;
            if (R > 55) pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\7.pNg");
        }

        //trạng thái vòng tròn xoay quanh con tro
        bool bTH1 = false;
        private void btnTH1_Click(object sender, EventArgs e)
        {
            bTH1 = !bTH1;
        }

        //trang thai thay doi kich thuoc duong line
        bool bTH2 = false;
        private void btnTH2_Click(object sender, EventArgs e)
        {
            bTH2 = !bTH2;
            bTH2 = !bTH2;
        }

        private int iDrawLineDacBiet = 20;
        private void btnGiam_Click(object sender, EventArgs e)
        {
            iDrawLineDacBiet -= 10;
        }
        private void btnTang_Click(object sender, EventArgs e)
        {
            iDrawLineDacBiet += 10;
        }
        private int iDrawLineDacBietTangGiam = 20;
        private void btnTien_Click(object sender, EventArgs e)
        {
            iDrawLineDacBietTangGiam++;
        }

        private void btnLui_Click(object sender, EventArgs e)
        {
            iDrawLineDacBietTangGiam--;
        }

        // menu

        private bool bVeVungBaoDong = false;
        private bool ChoPhepVe = false;
        private void btnVeVungBaoDong_Click(object sender, EventArgs e)
        {
            bVeVungBaoDong = !bVeVungBaoDong;
            ChoPhepVe = true;
        }
        //MANHINHRADAR
        private int h2 = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (NGUON)
                h2++;
            DISPLA.Visible = true;


            if (h2 == 2) { DISPLA.Text = "+PLOTTER"; pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\plott.pNg"); }
            if (h2 == 3)
            {
            }
        }

        private int z1 = 0;
        private void button41_Click_1(object sender, EventArgs e)
        {
            z1++;
            if (z1 == 1)
            {
                if (DICHUYEN1.Visible == true)
                {
                    DICHUYEN1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\HUONG1.pNg");
                }

                if (DICHUYEN2.Visible == true)
                {
                    DICHUYEN2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\BEN1212.pNg");
                }

                if (DICHUYEN3.Visible == true)
                {

                    DICHUYEN3.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\BEN123.pNg");
                }
            }
            if (z1 == 1) z1 = 0;
        }

        private int Th1 = 0;
        private void TH1_Click(object sender, EventArgs e)
        {
            Th1++;
            if (R > 55) Th1 = 2;
            if (Th1 == 1)
            {
                pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\MUA.pNg");

            }
            if (Th1 == 2)
            {
                pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\7.pNg");
                Th1 = 0;
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            dichuyen33.Visible = false;
        }

        private int al = 0;
        private void button53_Click(object sender, EventArgs e)
        {
            al++;
            if (al == 1)
                pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\al1.pNg");
            if (al == 2) { pboxRadar.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\7.pNg"); al = 0; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HNE = !HNE;
            HNE = true;
            KC = false;
            HAR = false; TOB = false;
            BAP = false; WAPY = false; BLOCK = false;
            KOH = false; EXID = false; NC = false;
            PES = false; NOM = false; NU = false; HAC = false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            TOB = !TOB;
            TOB = true;
            KC = false; HAR = false; HNE = false;
            BAP = false; WAPY = false; BLOCK = false;
            KOH = false; EXID = false; NC = false;
            PES = false; NOM = false; NU = false; HAC = false;

        }

        private void button31_Click(object sender, EventArgs e)
        {
            BLOCK = !BLOCK;
            BLOCK = true;
            KC = false; HAR = false; HNE = false;
            BAP = false; WAPY = false;
            KOH = false; EXID = false; NC = false;
            PES = false; NOM = false; NU = false; HAC = false;

        }

        private void button30_Click(object sender, EventArgs e)
        {
            WAPY = !WAPY;
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            BAP = !BAP;
            BAP = true;
            KC = false; HAR = false; HNE = false;
            WAPY = false; BLOCK = false;
            KOH = false; EXID = false; NC = false;
            PES = false; NOM = false; NU = false; HAC = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            HAR = !HAR;
            HAR = true;
            if (HAR == true)
            {
                KC = false; HNE = false; TOB = false;
                BAP = false; WAPY = false; BLOCK = false;
                KOH = false; EXID = false; NC = false;
                PES = false; NOM = false; NU = false; HAC = false;
                BTCD.BackColor = Color.Aquamarine;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }
            else
            {
                BTCD.BackColor = Color.Gray;
            }

            Form3 f2 = new Form3();
            f2.FormClosed += new FormClosedEventHandler(f2_FormClosed);
            f2.Show();

            this.Hide();
        }
        private void f2_FormClosed(object sender, EventArgs e)
        {
            this.Show();
        }
        // doi mau button
        private bool KC = false, HAR = false, HNE = false, TOB = false;
        private bool BAP = false, WAPY = false, BLOCK = false;
        private bool KOH = false, EXID = false, NC = false;

        private void btnAddObj_Click(object sender, EventArgs e)
        {
            _drawType = DrawType.VeDoiTuongDiChuyen;
        }

        private bool PES = false, NOM = false, NU = false, HAC = false;
        private void button4_Click(object sender, EventArgs e)
        {
            KC = !KC;
            KC = true;

            if (KC == true)
            {

                HAR = false; HNE = false; TOB = false;
                BAP = false; WAPY = false; BLOCK = false;
                KOH = false; EXID = false; NC = false;
                PES = false; NOM = false; NU = false; HAC = false;
                BTKC.BackColor = Color.Aquamarine;
                BTCD.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }
            else
            {

                BTKC.ForeColor = Color.White;
            }
        }


        ///// usedcontrol

        private void button19_Click_1(object sender, EventArgs e)
        {
            NC = !NC;
            NC = true;

            if (NC == true)
            {
                KOH = false;
                KC = false; HAR = false; HNE = false;
                BAP = false; WAPY = false; BLOCK = false;
                EXID = false;
                PES = false; NOM = false; NU = false; HAC = false;
                nutDK1.BackColor = Color.Aquamarine;
                BTSET.BackColor = Color.White;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }

            else
            {
                nutDK1.BackColor = Color.Gray;
            }
            control1 hc = new control1();
            Class1.showcontrol(hc, panelchinh);

        }
        //thuc don menu thu2
        private void button20_Click_1(object sender, EventArgs e)
        {
            PES = !PES;
            PES = true;

            if (PES == true)
            {
                KOH = false;
                KC = false; HAR = false; HNE = false;
                BAP = false; WAPY = false; BLOCK = false;
                EXID = false; NC = false;
                NOM = false; NU = false; HAC = false;
                BTDK2.BackColor = Color.Aquamarine;
                BTSET.BackColor = Color.White;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }
            control2 ha = new control2();
            Class1.showcontrol(ha, panelchinh);
            ha.ClickButton1 += ME_ClickButton;
            ha.ClickButton2 += HE_ClickButton;
            ha.ClickButton3 += LE2_ClickButton;

            ha.ClickButton4 += LE1_ClickButton;
            ha.ClickButton5 += EW_ClickButton;
            ha.ClickButton6 += LE230_ClickButton;
            ha.ClickButton7 += LE260_ClickButton;
            ha.ClickButton8 += LE2150_ClickButton;

            ha.ClickButton9 += LE1_ClickButton;
            ha.ClickButton10 += EW_ClickButton;

        }
        //CHE DO ME
        private void ME_ClickButton(object sender, EventArgs e)
        {
            BTME = !BTME;
            BTHE = false; BTLE2 = false;
            hienchedo.Text = "ME 30KM 2c";


        }
        //CHE DO HE
        private void HE_ClickButton(object sender, EventArgs e)
        {
            BTHE = !BTHE;
            BTHE = false; BTLE2 = false;
            hienchedo.Text = "HE 30KM 2c";
            t.Interval = 60;
        }
        //CHE DO LE2
        private bool BTLE2 = false, BTHE = false, BTME = false;
        private void LE2_ClickButton(object sender, EventArgs e)
        {
            BTHE = false; BTME = false;
            BTLE2 = !BTLE2;
            hienchedo.Text = "LE2 30KM 2c";
        }
        private void LE230_ClickButton(object sender, EventArgs e)
        {

            if (BTLE2 == true)
                hienchedo.Text = "LE2 30KM 2c";


        }
        private void LE260_ClickButton(object sender, EventArgs e)
        {
            if (BTLE2 == true)
                hienchedo.Text = "LE2 60KM 2c";
        }
        private void LE2150_ClickButton(object sender, EventArgs e)
        {
            if (BTLE2 == true)
                hienchedo.Text = "LE2 150KM 2c";
        }
        private void LE1_ClickButton(object sender, EventArgs e)
        {
            if (BTLE2 == true)
                hienchedo.Text = "zzz";
        }
        private void EW_ClickButton(object sender, EventArgs e)
        {
            hienchedo.Text = "zzz";
        }
        //

        private void button21_Click_1(object sender, EventArgs e)
        {
            NOM = !NOM;
            NOM = true;
            if (NOM == true)
            {

                KOH = false;
                KC = false; HAR = false; HNE = false;
                BAP = false; WAPY = false; BLOCK = false;
                EXID = false; NC = false;
                PES = false; NU = false; HAC = false;
                BTDK3.BackColor = Color.Aquamarine;
                BTSET.BackColor = Color.White;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }

            control3 hb = new control3();
            Class1.showcontrol(hb, panelchinh);
        }
        private void button27_Click(object sender, EventArgs e)
        {
            NU = !NU;
            NU = true;

            if (NU == true)
            {
                KOH = false;
                KC = false; HAR = false; HNE = false;
                BAP = false; WAPY = false; BLOCK = false;
                EXID = false; NC = false;
                PES = false; NOM = false; HAC = false;
                BTDK4.BackColor = Color.Aquamarine;
                BTSET.BackColor = Color.White;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }
            control4 hd = new control4();
            Class1.showcontrol(hd, panelchinh);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            HAC = !HAC;
            HAC = true;

            if (HAC == true)
            {
                KOH = false;
                KC = false; HAR = false; HNE = false;
                BAP = false; WAPY = false; BLOCK = false;
                EXID = false; NC = false;
                PES = false; NOM = false; NU = false;
                BTDK5.BackColor = Color.Aquamarine;
                BTSET.BackColor = Color.White;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
            }
            else
            {
                BTDK5.BackColor = Color.Gray;
            }
            control5 hf = new control5();
            Class1.showcontrol(hf, panelchinh);
        }
        //hien form 2
        private void button1_Click(object sender, EventArgs e)
        {
            KOH = !KOH;
            KOH = !true;
            KC = false; HAR = false; HNE = false;
            BAP = false; WAPY = false; BLOCK = false;
            EXID = false; NC = false;
            PES = false; NOM = false; NU = false; HAC = false;
            if (KOH == true)
            {
                BTSET.BackColor = Color.Aquamarine;
                BTCD.BackColor = Color.White;
                BTKC.BackColor = Color.White;
                BTSET.BackColor = Color.White;
                BTEXID.BackColor = Color.White;
                nutDK1.BackColor = Color.White;
                BTDK2.BackColor = Color.White;
                BTDK3.BackColor = Color.White;
                BTDK4.BackColor = Color.White;
                BTDK5.BackColor = Color.White;
            }
            FORMKIEMTRA f1 = new FORMKIEMTRA();
            f1.FormClosed += new FormClosedEventHandler(f1_FormClosed);
            f1.Show();
            this.Hide();
        }
        private void f1_FormClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            EXID = !EXID;
            EXID = true;
            KOH = false;
            KC = false; HAR = false; HNE = false;
            BAP = false; WAPY = false; BLOCK = false;
            NC = false;
            PES = false; NOM = false; NU = false; HAC = false;

        }
        #endregion
        // HIEN N VA E
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            currentPoint = new Point(
                e.Location.X + CommonConst.PointAlige.X,
                e.Location.Y + CommonConst.PointAlige.Y);

            if (iR > 0)
            {
                //a = (int)iR / 4;
                //b = (int)iR / 6;
            }
            if (CheckDrawEllipse(e.Location))
            {
                pboxRadar.Cursor = Cursors.Cross;
            }
            else
            {
                pboxRadar.Cursor = Cursors.Default;
            }
        }


        private bool bClickMouse = false;
        private bool bVVBDToaDo1 = true;
        //private bool mouseClick = false;
        public enum DrawType
        {
            TrangThaiNgung, VongTronQuanhConTro, ThayDoiKichThuocDuongLine, VeVungBaoDong, VeDoiTuongDiChuyen
        }

        private DrawType _drawType;
        private void PICBOX1_Click(object sender, EventArgs e)
        {
            if (_drawType != DrawType.TrangThaiNgung)
            {
                switch (_drawType)
                {
                    case DrawType.TrangThaiNgung:
                        break;
                    case DrawType.VongTronQuanhConTro:
                        break;
                    case DrawType.ThayDoiKichThuocDuongLine:
                        break;
                    case DrawType.VeVungBaoDong:
                        break;
                    case DrawType.VeDoiTuongDiChuyen:
                        pnlObjTest.Tag = null;
                        objMove = null;
                        objMove = VeDoiTuongDiChuyenHelper.Draw(pboxRadar);
                        break;
                    default:
                        break;
                }
            }


            if (bTH1 || bTH2)
            {
                ToaDoChuotKhiAnButton.X = currentPoint.X;
                ToaDoChuotKhiAnButton.Y = currentPoint.Y;
                bClickMouse = true;
            }
            if (bVeVungBaoDong && ChoPhepVe)
            {
                if (bVVBDToaDo1)
                {
                    ToaDoChuotVeVungBaoDong1.X = currentPoint.X;
                    ToaDoChuotVeVungBaoDong1.Y = currentPoint.Y;
                    bVVBDToaDo1 = false;
                }
                else
                {
                    ToaDoChuotVeVungBaoDong2.X = currentPoint.X;
                    ToaDoChuotVeVungBaoDong2.Y = currentPoint.Y;
                    bVVBDToaDo1 = true;
                    ChoPhepVe = false;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            isArcDrawer = !isArcDrawer;
        }
        private void pboxRadar_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (arcDrawers.Count)
                {
                    case 0:
                    case 1:
                    case 2:
                        arcDrawers.Add(ArcDrawer.Draw(pboxRadar, _centerPoint, ArcType.FULL));
                        break;
                    case 3:
                        arcDrawers.Add(ArcDrawer.Draw(pboxRadar, _centerPoint, ArcType.HALF));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}








