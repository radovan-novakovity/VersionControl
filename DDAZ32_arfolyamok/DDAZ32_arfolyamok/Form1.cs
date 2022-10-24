using DDAZ32_arfolyamok.Entities;
using DDAZ32_arfolyamok.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace DDAZ32_arfolyamok
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();

        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            GetCurrencies();
            RefreshData();

        }
        // 3. feladat
        private string GetMoneyExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                //currencyNames = result,
                // 7. feladat
                //currencyNames = comboBox1.SelectedItem.ToString(),
                //startDate = "2020-01-01",
                // 7. feladat
                startDate = Convert.ToString(dateTimePicker1.Value),
                //endDate = "2020-06-30"
                // 7. feladat
                endDate = Convert.ToString(dateTimePicker2.Value)
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            return result;
        }

        // 5. feladat
        private void GetXMLData(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];

                if (childElement == null)
                    continue;

                rate.Currency = childElement.GetAttribute("curr");

                //Currencies.Add(childElement.GetAttribute("curr"));

                //foreach (XmlNode chelement in childElement)
                //{
                //    Currencies.Add(chelement.Attributes.Item);
                //}

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }

        }

        // 6. feladat
        private void VisualizeData()
        {
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        // 7. feladat
        private void RefreshData()
        {
            Rates.Clear();

            // var output = GetMoneyExchangeRates();
            // GetXMLData(output);

            // GetXMLData(GetMoneyExchangeRates());
            GetXMLData(GetMoneyExchangeRates());
            VisualizeData();
            dataGridView1.DataSource = Rates;
            chartRateData.DataSource = Rates;
            comboBox1.DataSource = Currencies;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        //8. feladat
        private string GetCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);

            var result2 = response.GetCurrenciesResult;

            return result2;
        }
    }
}
