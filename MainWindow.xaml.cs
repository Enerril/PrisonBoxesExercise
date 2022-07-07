using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PrisonBoxesExercise
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] boxes;
        int[] prisoners;
        int i;
        int _simCount;
        int _boxesPer;
        int temp;
        int temp2;
        int iter1;
        int iter2;
        int iter3;
        int iter4;
        int randomBox = 0;
        int nextBox=0;
        int boxValue;

        int _luckyPrisoners=0;
        int _unluckyPrisoners=0;

        int _statAllLuckyPr;
        int _statAllUnluckyPr;

        int _prWin;
        int _prLose;
        

        int _OverallSimCount;
        float _SurvRate;
        float tempf;
        List<int> boxesList = new List<int>();
        List<int> actualBoxesList = new List<int>();
        List<int> checkedBoxesList = new List<int>();

        bool initLoopVal;


        Random rnd;
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            boxes = new int[int.Parse(prCount.Text)];
            prisoners = new int[int.Parse(prCount.Text)];
            _simCount = int.Parse(simCount.Text);
            _OverallSimCount += _simCount;
            rnd = new Random();

            

            for (int p = 0; p < _simCount; p++)
            {
                

                for (i = 0; i < boxes.Length; i++)
                {
                    boxes[i] = i;
                }

                Shuffle(rnd,ref boxes);

                StartRandomSimulation(ref prisoners, ref boxes);

              


            }

            CalculateChances();

            UpdateTextValues();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            boxes = new int[int.Parse(prCount.Text)];
            prisoners = new int[int.Parse(prCount.Text)];
            _simCount = int.Parse(simCount.Text);
            _OverallSimCount += _simCount;
            rnd = new Random();



            for (int p = 0; p < _simCount; p++)
            {


                for (i = 0; i < boxes.Length; i++)
                {
                    boxes[i] = i;
                }

                Shuffle(rnd, ref boxes);

                StartRandomSimulation2(ref prisoners, ref boxes);




            }

            CalculateChances2();

            UpdateTextValues();
        }


        private void StartRandomSimulation2(ref int[] prisoners, ref int[] boxes)
        {
            _luckyPrisoners = 0;
            _unluckyPrisoners = 0;
            _boxesPer = int.Parse(boxesPer.Text);
            boxesList.Clear();

            for (iter3 = 0; iter3 < boxes.Length; iter3++)
            {
                boxesList.Add(boxes[iter3]);
            }
            // Debug.WriteLine(_boxesPer);

            for (iter2 = 0; iter2 < prisoners.Length; iter2++)
            {
                actualBoxesList.Clear();
                checkedBoxesList.Clear();
                nextBox = 0;
                initLoopVal = false;

                for (iter4 = 0; iter4 < boxesList.Count; iter4++)
                {
                    actualBoxesList.Add(boxesList.ElementAt(iter4));
                }

                for (iter1 = 0; iter1 < _boxesPer; iter1++)
                {
                    //Debug.WriteLine(iter1);
                    if (!initLoopVal)
                    {
                        randomBox = iter2;
                        boxValue = actualBoxesList.ElementAt(randomBox);
                        initLoopVal = true;
                    }
                    else
                    {
                        boxValue = actualBoxesList.ElementAt(nextBox);
                    }
                    

                    if (boxValue == iter2)
                    {
                        //SUCCESS
                        //save state!
                        _luckyPrisoners++;
                        _statAllLuckyPr++;
                        break;
                    }

                    if (iter1 == (_boxesPer - 1))
                    {
                        _statAllUnluckyPr++;
                        _unluckyPrisoners++;

                    }

                    nextBox = boxValue;
                    //actualBoxesList.RemoveAt(randomBox);
                }



            }

            if (_unluckyPrisoners > 0)
            {

                _prLose++;
            }
            else
            {
                _prWin++;
            }


        }


        private void StartRandomSimulation(ref int[]prisoners,ref int[] boxes)
        {
            _luckyPrisoners = 0;
            _unluckyPrisoners = 0;
            _boxesPer = int.Parse(boxesPer.Text);
            boxesList.Clear();

            for (iter3 = 0; iter3 < boxes.Length; iter3++)
            {
                boxesList.Add(boxes[iter3]);
            }
            // Debug.WriteLine(_boxesPer);

            for (iter2 = 0; iter2 < prisoners.Length; iter2++)
            {
                actualBoxesList.Clear();
                checkedBoxesList.Clear();

                for (iter4 = 0; iter4 < boxesList.Count; iter4++)
                {
                    actualBoxesList.Add(boxesList.ElementAt(iter4));
                }
                                
                for (iter1 = 0; iter1 < _boxesPer; iter1++)
                {
                    //Debug.WriteLine(iter1);
                    
                    randomBox = rnd.Next(0, actualBoxesList.Count);
                  
                    if (actualBoxesList.ElementAt(randomBox) == iter2)
                    {
                        //SUCCESS
                        //save state!
                        _luckyPrisoners++;
                        _statAllLuckyPr++;
                        break;
                    }

                    if (iter1== (_boxesPer-1))
                    {
                        _statAllUnluckyPr++;
                        _unluckyPrisoners++;
                        
                    }

                    actualBoxesList.RemoveAt(randomBox);
                }


                
            }

            if (_unluckyPrisoners >0)
            {
                
                _prLose++;
            }
            else
            {
                _prWin++;
            }

            
        }

        void Shuffle(Random rnd, ref int[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        private void ClearSingleStats(object sender, RoutedEventArgs e)
        {
            _unluckyPrisoners = 0;
            _luckyPrisoners = 0;

            UpdateTextValues();
        }

        private void ClearOverallStats(object sender, RoutedEventArgs e)
        {
            _statAllLuckyPr = 0;
            _statAllUnluckyPr = 0;
            _OverallSimCount = 0;
            _prWin = 0;
            _prLose = 0;
            _SurvRate = 0;
            UpdateTextValues();
        }

        void UpdateTextValues()
        {
            lPr.Text = _luckyPrisoners.ToString();
            unLPr.Text = _unluckyPrisoners.ToString();
            OverallLucky.Text = _statAllLuckyPr.ToString();
            OverallUnlucky.Text = _statAllUnluckyPr.ToString();
            OverallSimCount.Text = _OverallSimCount.ToString();
            SurvRate.Text = _SurvRate.ToString();
            prWin.Text = _prWin.ToString();
            prLose.Text = _prLose.ToString();
        }

        void CalculateChances()
        {

            chancePrec_singlePr.Text = ((float.Parse(boxesPer.Text) / float.Parse(prCount.Text))*100).ToString();
            chancePrec_allSurv.Text = ((Math.Pow((double.Parse(boxesPer.Text) / double.Parse(prCount.Text)), double.Parse(prCount.Text)))*100).ToString();

            _SurvRate = ((float)(_prWin) / (float)(_OverallSimCount))*100;

            UpdateTextValues();

        }

        void CalculateChances2()
        {

            chancePrec_singlePr.Text = ((float.Parse(boxesPer.Text) / float.Parse(prCount.Text)) * 100).ToString();

            temp = int.Parse(prCount.Text);
            temp2 = int.Parse(boxesPer.Text);
            tempf = 0;
            for (iter1 = temp2+1; iter1 <= temp; iter1++)
            {
                tempf += 1 / (float)iter1;
                //Debug.WriteLine(tempf);
            }

            
            chancePrec_allSurv.Text = (((float)(1 - tempf)) * 100).ToString();

            _SurvRate = ((float)(_prWin) / (float)(_OverallSimCount)) * 100;

            UpdateTextValues();

        }

    }





}
