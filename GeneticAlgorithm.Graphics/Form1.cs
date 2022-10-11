using GeneticAlgorithm.Parameters;
using MindFusion.Charting;

namespace GeneticAlgorithm.Graphics
{
    public partial class Form1 : Form
    {
        private AlgorithmParameters parameters;
        public Form1()
        {
            InitializeComponent();
            parameters = new AlgorithmParameters();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            DataBoundSeries
            System.Drawing.Graphics graphics = pictureBox2.CreateGraphics();
            Pen pen = new Pen(Color.Red, 3f);

            Point[] points = new Point[10000];
            int intervalBetweenPoints = 10000 / parameters.MaxAmountOfGenerations;

            Algorithm algorithm = new(parameters);

            var result = algorithm.Run();

            for (int i = 0; i < result.Count(); i++)
            {
                var population = result.ElementAt(i);

                for (int j = 0; j <= intervalBetweenPoints; j++)
                    points[i + j] = new Point(i, Convert.ToInt32(population.MaxFitness));
            }

            graphics.DrawLines(pen, points);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}