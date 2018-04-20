using exam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exam
{
    public partial class Form1 : Form
    {
        DataTable dataTableSport = null, dataTableCountry = null, dataTableAthlete = null, dataTableOlympic = null, dataTableMedal = null, dataTableType = null;
        bool flagCountry = false, flagSport = false, flagAthlete = false, flagResult = false;

        public Form1()
        {
            InitializeComponent();
            Fill(dataGridAthletes);
            Fill(dataGridCountry);
            Fill(dataGridSport);
            Fill(dataGridResult);
            SelectRefresh(cbCountryS);
            SelectRefresh(cbCountryO);
            SelectRefresh(cbSportS);
            SelectRefresh(cbAthleteO);
            SelectRefresh(cbMedal);
            SelectRefresh(cbType);
            SelectRefresh(lwPicture);
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        }

        private void SelectRefresh(ListView lwPicture)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if (lwPicture.Name.ToString() == "lwPicture")
                {
                    ImageList imagelist = new ImageList();
                    imagelist.ImageSize = new Size(20, 20);
                    foreach (var item in db.Pictures)
                        imagelist.Images.Add(Image.FromFile(item.Photo));
                    lwPicture.View = View.LargeIcon;
                    lwPicture.View = View.Tile;
                    lwPicture.LargeImageList = imagelist;
                    foreach (var item in db.Pictures)
                        lwPicture.Items.Add(new ListViewItem(item.Photo, item.IDPicture - 1));
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://olympteka.ru/olymp/pyeongchang2018/shedule.html");
        }

        private void Fill(DataGridView dataGrid)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if (dataGrid.Name.ToString() == "dataGridAthletes")
                {
                    dataTableAthlete = new DataTable();
                    dataTableAthlete.Columns.Add("IDAthletes");
                    dataTableAthlete.Columns.Add("FirstName");
                    dataTableAthlete.Columns.Add("LastName");
                    dataTableAthlete.Columns.Add("Country");
                    dataTableAthlete.Columns.Add("Sport");
                    dataTableAthlete.Columns.Add("Photo", typeof(byte[]));

                    var list = from a in db.Athletes
                               from c in db.Country
                               from s in db.Sports
                               from p in db.Pictures
                               where a.IDSport == s.IDSport && a.IDCountry == c.IDCountry && p.IDPicture == a.IDPicture
                               select new { IDAthletes = a.IDAthlete, FirstName = a.FirstName, LastName = a.LastName, Country = c.Country1, Sports = s.ТNameSport, Birthday = a.Data, Photo = p.Photo };
                    foreach (var item in list)
                    {
                        DataRow dataRow = dataTableAthlete.NewRow();
                        dataRow["IDAthletes"] = item.IDAthletes;
                        dataRow["FirstName"] = item.FirstName;
                        dataRow["LastName"] = item.LastName;
                        dataRow["Country"] = item.Country;
                        dataRow["Sport"] = item.Sports;
                        dataRow["Photo"] = imageToByteArray(Image.FromFile(item.Photo));
                        dataTableAthlete.Rows.Add(dataRow);
                    }
                    dataGridAthletes.DataSource = null;
                    dataGridAthletes.DataSource = dataTableAthlete;
                    dataGridAthletes.Columns[0].Visible = false;
                }
                else if (dataGrid.Name.ToString() == "dataGridCountry")
                {
                    dataTableCountry = new DataTable();
                    dataTableCountry.Columns.Add("IDCountry");
                    dataTableCountry.Columns.Add("Country");
                    foreach (var item in db.Country)
                    {
                        DataRow dataRow = dataTableCountry.NewRow();
                        dataRow["IDCountry"] = item.IDCountry;
                        dataRow["Country"] = item.Country1;
                        dataTableCountry.Rows.Add(dataRow);
                    }
                    dataGridCountry.DataSource = null;
                    dataGridCountry.DataSource = dataTableCountry;
                    dataGridCountry.Columns[0].Visible = false;
                }
                else if (dataGrid.Name.ToString() == "dataGridSport")
                {
                    dataTableSport = new DataTable();
                    dataTableSport.Columns.Add("IDSport");
                    dataTableSport.Columns.Add("Sport");
                    foreach (var item in db.Sports)
                    {
                        DataRow dataRow = dataTableSport.NewRow();
                        dataRow["IDSport"] = item.IDSport;
                        dataRow["Sport"] = item.ТNameSport;
                        dataTableSport.Rows.Add(dataRow);
                    }
                    dataGridSport.DataSource = null;
                    dataGridSport.DataSource = dataTableSport;
                    dataGridSport.Columns[0].Visible = false;
                }
                else if (dataGrid.Name.ToString() == "dataGridResult")
                {
                    dataTableOlympic = new DataTable();
                    dataTableOlympic.Columns.Add("IDOlympic");
                    dataTableOlympic.Columns.Add("Country");
                    dataTableOlympic.Columns.Add("Year");
                    dataTableOlympic.Columns.Add("Athlete");
                    dataTableOlympic.Columns.Add("Medal");
                    dataTableOlympic.Columns.Add("Type");

                    var list = from r in db.Results
                               from c in db.Country
                               from a in db.Athletes
                               from m in db.Medals
                               from t in db.TypeOfOlympiads
                               where r.IDCountry == c.IDCountry && r.IDMedal == m.IDMedal && r.IDAthlete == a.IDAthlete && r.IDTypeOfOlympiad == t.IDTypeOfOlympiad
                               select new { IDOlympic = r.IDResult, Country = c.Country1, Year = r.Year, Athlete = a.FirstName, Medals = m.Medal, Type = t.TypeOfOlympiad };

                    foreach (var item in list)
                    {
                        DataRow dataRow = dataTableOlympic.NewRow();
                        dataRow["IDOlympic"] = item.IDOlympic;
                        dataRow["Country"] = item.Country;
                        dataRow["Year"] = item.Year;
                        dataRow["Athlete"] = item.Athlete;
                        dataRow["Medal"] = item.Medals;
                        dataRow["Type"] = item.Type;
                        dataTableOlympic.Rows.Add(dataRow);
                    }
                    dataGridResult.DataSource = null;
                    dataGridResult.DataSource = dataTableOlympic;
                    dataGridResult.Columns[0].Visible = false;
                }
                Statics();
            }
        }

        private object imageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if ((sender as Button).Name.ToString() == "btnDeleteCountry")
                {
                    if (dataGridCountry.SelectedRows.Count > 0)
                    {
                        int index = dataGridCountry.SelectedRows[0].Index;
                        int id = Convert.ToInt32(dataGridCountry[0, index].Value);
                        Country country = db.Country.Find(id);
                        db.Country.Remove(country);
                        db.SaveChanges();
                        Fill(dataGridCountry);
                    }
                    SelectRefresh(cbCountryS);
                    SelectRefresh(cbCountryO);
                }
                else if ((sender as Button).Name.ToString() == "btnDeleteSport")
                {
                    if (dataGridSport.SelectedRows.Count > 0)
                    {
                        int index = dataGridSport.SelectedRows[0].Index;
                        int id = Convert.ToInt32(dataGridSport[0, index].Value);
                        Sports sports = db.Sports.Find(id);
                        db.Sports.Remove(sports);
                        db.SaveChanges();
                        Fill(dataGridSport);
                    }
                    SelectRefresh(cbSportS);
                }
                else if ((sender as Button).Name.ToString() == "btnDeleteAthletes")
                {
                    if (dataGridAthletes.SelectedRows.Count > 0)
                    {
                        int index = dataGridAthletes.SelectedRows[0].Index;
                        int id = Convert.ToInt32(dataGridAthletes[0, index].Value);
                        Athletes athletes = db.Athletes.Find(id);
                        db.Athletes.Remove(athletes);
                        db.SaveChanges();
                        Fill(dataGridAthletes);
                    }
                    SelectRefresh(cbAthleteO);
                }
                else if ((sender as Button).Name.ToString() == "btnDeleteResult")
                {
                    if (dataGridResult.SelectedRows.Count > 0)
                    {
                        int index = dataGridResult.SelectedRows[0].Index;
                        int id = Convert.ToInt32(dataGridResult[0, index].Value);
                        Results results = db.Results.Find(id);
                        db.Results.Remove(results);
                        db.SaveChanges();
                        Fill(dataGridResult);
                    }
                }
            }
        }

        private void SelectRefresh(ComboBox cb)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if (cb.Name.ToString() == "cbCountryO")
                {
                    cb.DataSource = null;
                    cb.DataSource = dataTableCountry;
                    cb.ValueMember = "IDCountry";
                    cb.DisplayMember = "Country";
                }
                else if (cb.Name.ToString() == "cbCountryS")
                {
                    cb.DataSource = null;
                    cb.DataSource = dataTableCountry;
                    cb.ValueMember = "IDCountry";
                    cb.DisplayMember = "Country";
                }
                else if (cb.Name.ToString() == "cbSportS")
                {
                    cb.DataSource = null;
                    cb.DataSource = dataTableSport;
                    cb.ValueMember = "IDSport";
                    cb.DisplayMember = "Sport";
                }
                else if (cb.Name.ToString() == "cbAthleteO")
                {
                    cb.DataSource = null;
                    cb.DataSource = dataTableAthlete;
                    cb.ValueMember = "IDAthletes";
                    cb.DisplayMember = "FirstName";
                }
                else if (cb.Name.ToString() == "cbMedal")
                {
                    dataTableMedal = new DataTable();
                    dataTableMedal.Columns.Add("IDMedal");
                    dataTableMedal.Columns.Add("Medal");
                    foreach (var item in db.Medals)
                    {
                        DataRow dataRow = dataTableMedal.NewRow();
                        dataRow["IDMedal"] = item.IDMedal;
                        dataRow["Medal"] = item.Medal;
                        dataTableMedal.Rows.Add(dataRow);
                    }
                    cb.DataSource = null;
                    cb.DataSource = dataTableMedal;
                    cb.ValueMember = "IDMedal";
                    cb.DisplayMember = "Medal";
                }
                else if (cb.Name.ToString() == "cbType")
                {
                    dataTableType = new DataTable();
                    dataTableType.Columns.Add("IDType");
                    dataTableType.Columns.Add("Type");
                    foreach (var item in db.TypeOfOlympiads)
                    {
                        DataRow dataRow = dataTableType.NewRow();
                        dataRow["IDType"] = item.IDTypeOfOlympiad;
                        dataRow["Type"] = item.TypeOfOlympiad;
                        dataTableType.Rows.Add(dataRow);
                    }
                    cb.DataSource = null;
                    cb.DataSource = dataTableType;
                    cb.ValueMember = "IDType";
                    cb.DisplayMember = "Type";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if ((sender as Button).Name.ToString() == "btnAddCountry")
                {
                    if (flagCountry != true)
                    {
                        Country country = new Country { Country1 = txtCountryC.Text.ToString() };
                        db.Country.Add(country);
                    }
                    else
                    {
                        flagCountry = false;
                        int index = dataGridCountry.CurrentRow.Index;
                        int id = Convert.ToInt32(dataGridCountry[0, index].Value);
                        db.Country.Find(id).Country1 = txtCountryC.Text.ToString();
                    }
                    txtCountryC.Text = null;
                    db.SaveChanges();
                    Fill(dataGridCountry);
                    SelectRefresh(cbCountryS);
                    SelectRefresh(cbCountryO);
                }
                else if ((sender as Button).Name.ToString() == "btnAddSport")
                {
                    if (flagSport != true)
                    {
                        Sports sport = new Sports { ТNameSport = txtSportS.Text.ToString() };
                        db.Sports.Add(sport);
                    }
                    else
                    {
                        flagSport = false;
                        int index = dataGridSport.CurrentRow.Index;
                        int id = Convert.ToInt32(dataGridSport[0, index].Value);
                        db.Sports.Find(id).ТNameSport = txtSportS.Text.ToString();
                    }
                    txtSportS.Text = null;
                    db.SaveChanges();
                    Fill(dataGridSport);
                    SelectRefresh(cbSportS);
                }
                else if ((sender as Button).Name.ToString() == "btnAddAthletes")
                {
                    if (flagAthlete != true)
                    {
                        Athletes athletes = new Athletes();
                        athletes.FirstName = txtFirstName.Text.ToString();
                        athletes.LastName = txtLastName.Text.ToString();
                        athletes.IDCountry = Convert.ToInt32(cbCountryS.SelectedValue);
                        athletes.IDSport = Convert.ToInt32(cbSportS.SelectedValue);
                        athletes.Data = dpBirthday.Value;
                        athletes.IDPicture = lwPicture.FocusedItem.Index + 1;
                        db.Athletes.Add(athletes);
                    }
                    else
                    {
                        flagAthlete = false;
                        int index = dataGridAthletes.CurrentRow.Index;
                        int id = Convert.ToInt32(dataGridAthletes[0, index].Value);
                        db.Athletes.Find(id).FirstName = txtFirstName.Text.ToString();
                        db.Athletes.Find(id).LastName = txtLastName.Text.ToString();
                        db.Athletes.Find(id).IDCountry = Convert.ToInt32(cbCountryS.SelectedValue);
                        db.Athletes.Find(id).IDSport = Convert.ToInt32(cbSportS.SelectedValue);
                        db.Athletes.Find(id).Data = dpBirthday.Value;
                    }
                    txtFirstName.Text = null;
                    txtLastName.Text = null;
                    db.SaveChanges();
                    Fill(dataGridAthletes);
                    SelectRefresh(cbAthleteO);
                }
                else if ((sender as Button).Name.ToString() == "btnAddResult")
                {
                    if (flagResult != true)
                    {
                        Results result = new Results();
                        result.IDCountry = Convert.ToInt32(cbCountryO.SelectedValue);
                        result.Year = Convert.ToInt32(txtYear.Text);
                        result.City = txtCity.Text.ToString();
                        result.IDAthlete = Convert.ToInt32(cbAthleteO.SelectedValue);
                        result.IDMedal = Convert.ToInt32(cbMedal.SelectedValue);
                        result.IDTypeOfOlympiad = Convert.ToInt32(cbType.SelectedValue);
                        db.Results.Add(result);
                    }
                    else
                    {
                        int index = dataGridResult.CurrentRow.Index;
                        int id = Convert.ToInt32(dataGridResult[0, index].Value);
                        db.Results.Find(id).IDCountry = Convert.ToInt32(cbCountryO.SelectedValue);
                        db.Results.Find(id).City = txtCity.Text.ToString();
                        db.Results.Find(id).Year = Convert.ToInt32(txtYear.Text);
                        db.Results.Find(id).IDAthlete = Convert.ToInt32(cbAthleteO.SelectedValue);
                        db.Results.Find(id).IDMedal = Convert.ToInt32(cbMedal.SelectedValue);
                        db.Results.Find(id).IDTypeOfOlympiad = Convert.ToInt32(cbType.SelectedValue);
                    }
                    txtYear.Text = null;
                    txtCity.Text = null;
                    db.SaveChanges();
                    Fill(dataGridResult);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                if ((sender as Button).Name.ToString() == "btnEditCountry")
                {
                    int index = dataGridCountry.CurrentRow.Index;
                    int id = Convert.ToInt32(dataGridCountry[0, index].Value);
                    txtCountryC.Text = db.Country.Find(id).Country1.ToString();
                    flagCountry = true;
                }
                else if ((sender as Button).Name.ToString() == "btnEditSport")
                {
                    int index = dataGridSport.CurrentRow.Index;
                    int id = Convert.ToInt32(dataGridSport[0, index].Value);
                    txtSportS.Text = db.Sports.Find(id).ТNameSport.ToString();
                    flagSport = true;
                }
                else if ((sender as Button).Name.ToString() == "btnEditAthletes")
                {
                    int index = dataGridAthletes.CurrentRow.Index;
                    int id = Convert.ToInt32(dataGridAthletes[0, index].Value);
                    txtFirstName.Text = db.Athletes.Find(id).FirstName.ToString();
                    txtLastName.Text = db.Athletes.Find(id).LastName.ToString();
                    cbCountryS.SelectedValue = db.Athletes.Find(id).IDCountry;
                    cbSportS.SelectedValue = db.Athletes.Find(id).IDSport;
                    dpBirthday.Value = db.Athletes.Find(id).Data;
                    flagAthlete = true;
                }
                else if ((sender as Button).Name.ToString() == "btnEditResult")
                {
                    int index = dataGridResult.CurrentRow.Index;
                    int id = Convert.ToInt32(dataGridResult[0, index].Value);
                    cbCountryO.SelectedValue = db.Results.Find(id).IDCountry;
                    txtCity.Text = db.Results.Find(id).City.ToString();
                    txtYear.Text = db.Results.Find(id).Year.ToString();
                    cbAthleteO.SelectedValue = db.Results.Find(id).IDAthlete;
                    cbMedal.SelectedValue = db.Results.Find(id).IDMedal;
                    cbType.SelectedValue = db.Results.Find(id).IDTypeOfOlympiad;
                    flagResult = true;
                }
                Statics();
            }
        }


        private void Statics()
        {
            using (OlympicsEntities3 db = new OlympicsEntities3())
            {
                //название страны, которая чаще всех была хозяйкой олимпиады;
                var nameCountry = (
                                   from r in db.Results
                                   from c in db.Country
                                   where r.IDCountry == c.IDCountry
                                   group r by c.Country1 into c
                                   orderby c.Count() descending
                                   select (c.Key)
                                   ).FirstOrDefault().ToString();
                label15.Text = nameCountry;

                //таблицу медального зачета по странам по конкретной олимпиаде, за всю историю олимпиад;
                var Medal = (from r in db.Results
                             from c in db.Country
                             where r.IDCountry == c.IDCountry && r.IDMedal != 4
                             orderby r.Year descending
                             select (
                             new { Country = c.Country1, Year = r.Year, Medal = r.Medals.Medal }
                             )
                         ).ToList();
                dataGridViewMedal.DataSource = null;
                dataGridViewMedal.DataSource = Medal;

                //статистику выступления конкретной страны на конкретной олимпиаде, за всю историю олимпиад.
                var MedalAll = (from r in db.Results
                                from c in db.Country
                                where r.IDCountry == c.IDCountry && r.IDMedal != 4
                                group r by new { c.Country1, r.Year } into m
                                orderby m.Key.Year descending
                                select (
                                new { Country = m.Key.Country1, Year = m.Key.Year, AmountAll = m.Count() }
                                )
                               ).ToList();
                dataGridViewMedalAll.DataSource = null;
                dataGridViewMedalAll.DataSource = MedalAll;

                //медалистов по разным видам спорта по конкретной олимпиаде, за всю историю олимпиад;
                var NameSport = (from r in db.Results
                                 from s in db.Sports
                                 from a in db.Athletes
                                 where r.IDAthlete == a.IDAthlete && s.IDSport == a.IDSport && r.IDMedal != 4
                                 orderby r.Year descending
                                 select (
                                 new { Year = r.Year, Sport = a.Sports.ТNameSport, Medal = r.Medals.Medal, Name = a.FirstName + " " + a.LastName }
                                 )
                                ).ToList();
                dataGridViewMedalists.DataSource = null;
                dataGridViewMedalists.DataSource = NameSport;

                //страну, которая собрала больше всего золотых медалей на конкретной олимпиаде, за всю историю олимпиад;
                var GoldMedal = (from r in db.Results
                                 from c in db.Country
                                 where r.IDCountry == c.IDCountry && r.IDMedal == 1
                                 group r by new { c.Country1, r.Year } into m
                                 orderby m.Count() descending
                                 select (m.Key.Country1)
                                ).FirstOrDefault().ToString();
                label16.Text = GoldMedal;

                //спортсмена, который выиграл больше всего золотых медалей в конкретном виде спорта;
                var GoldMedalSport = (from r in db.Results
                                      from a in db.Athletes
                                      from s in db.Sports
                                      where s.IDSport == a.IDSport && r.IDAthlete == a.IDAthlete && r.IDMedal == 1
                                      group r by new { a.FirstName, a.LastName, s.ТNameSport } into m
                                      orderby m.Count() descending
                                      select (m.Key.FirstName + " " + m.Key.LastName)
                                    ).FirstOrDefault().ToString();
                label17.Text = GoldMedalSport;

                //состав олимпиадной команды спортсменов конкретной страны;
                var Command = (from r in db.Results
                               from c in db.Country
                               from a in db.Athletes
                               from s in db.Sports
                               where a.IDCountry == c.IDCountry && a.IDAthlete == r.IDAthlete && s.IDSport == a.IDSport && r.Year == 2018
                               orderby a.IDCountry
                               select (
                                new { Country = c.Country1, Name = a.FirstName + " " + a.LastName, Sport = s.ТNameSport }
                               )
                              ).ToList();


                dataGridViewCommand.DataSource = null;
                dataGridViewCommand.DataSource = Command;

            }
        }
    }
}
