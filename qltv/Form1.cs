using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace qltv
{
    public partial class Form1 : Form
    {
        string id;
        public Form1()
        {
            InitializeComponent();        
            getData();
        }
        //
        public void getData()
        {
            var client = new RestClient("http://localhost:1337/Saches");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZDQ1NTk3MjhiZTA1MzJhZDg3NDFjODMiLCJpZCI6IjVkNDU1OTcyOGJlMDUzMmFkODc0MWM4MyIsImlhdCI6MTU2NzY3MTEwMSwiZXhwIjoxNTcwMjYzMTAxfQ.xUdryjfLBwNHMb7LHzIwrVJaML_oOMcT5yL7JTLY1V8");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<Sach> oMyclass = JsonConvert.DeserializeObject<List<Sach>>(response.Content.Trim());
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.View = View.Details;
            listView1.Clear();
            listView1.Columns.Add("id",0);
            listView1.Columns.Add("Mã Sách", 60);
            listView1.Columns.Add("Tên Sách", 200);
            listView1.Columns.Add("Tên tác giả", 130);
            listView1.Columns.Add("Tên NXB", 190);
            listView1.Columns.Add("Năm NXB", 60);
            for (int i = 0; i < oMyclass.Count; i++)
            {
                ListViewItem lv = new ListViewItem(oMyclass[i].id);
                lv.SubItems.Add(oMyclass[i].MaSach);               
                lv.SubItems.Add(oMyclass[i].TenSach);
                lv.SubItems.Add(oMyclass[i].TenTacGia);
                lv.SubItems.Add(oMyclass[i].TenNXB);
                lv.SubItems.Add(oMyclass[i].NamXB);
                listView1.Items.Add(lv);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if(listView1.SelectedItems.Count>0)
            {
                id = listView1.SelectedItems[0].SubItems[0].Text;
                txtMaSach.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtTenSach.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtTacGia.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtNhaXB.Text = listView1.SelectedItems[0].SubItems[4].Text;
                txtNamXB.Text = listView1.SelectedItems[0].SubItems[5].Text;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://localhost:1337/saches");
            client.Timeout = -1;
            string mas = txtMaSach.Text.ToString().Trim();
            string tens = txtTenSach.Text.ToString().Trim();
            string tentg = txtTacGia.Text.ToString().Trim();
            string tennxb = txtNhaXB.Text.ToString().Trim();
            int namxb = int.Parse(txtNamXB.Text.ToString().Trim());
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n\t\"MaSach\": \"" + mas + "\",\r\n    \"TenSach\": \"" + tens + "\",\r\n    \"TenTacGia\": \"" + tentg + "\",\r\n    \"TenNXB\": \"" + tennxb + "\",\r\n    \"NamXB\": \"" + namxb + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MessageBox.Show("Thêm mới thành công");
            getData();        
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://localhost:1337/Saches/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZDQ1NTk3MjhiZTA1MzJhZDg3NDFjODMiLCJpZCI6IjVkNDU1OTcyOGJlMDUzMmFkODc0MWM4MyIsImlhdCI6MTU2NzY3MTEwMSwiZXhwIjoxNTcwMjYzMTAxfQ.xUdryjfLBwNHMb7LHzIwrVJaML_oOMcT5yL7JTLY1V8");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MessageBox.Show("Bạn đã xoá bản ghi có id là: " + id);
            getData();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://localhost:1337/Saches/"+id);
            client.Timeout = -1;
            //string mas = txtMaSach.Text.ToString().Trim();
            string tens = txtTenSach.Text.ToString().Trim();
            string tentg = txtTacGia.Text.ToString().Trim();
            string tennxb = txtNhaXB.Text.ToString().Trim();
            int namxb = int.Parse(txtNamXB.Text.ToString().Trim());
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZDQ1NTk3MjhiZTA1MzJhZDg3NDFjODMiLCJpZCI6IjVkNDU1OTcyOGJlMDUzMmFkODc0MWM4MyIsImlhdCI6MTU2NzY3MTEwMSwiZXhwIjoxNTcwMjYzMTAxfQ.xUdryjfLBwNHMb7LHzIwrVJaML_oOMcT5yL7JTLY1V8");
            request.AddParameter("application/json", "{\r\n    \"TenSach\": \"" + tens + "\",\r\n    \"TenTacGia\": \"" + tentg + "\",\r\n    \"TenNXB\": \"" + tennxb + "\",\r\n    \"NamXB\": \"" + namxb + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MessageBox.Show("Bạn vừa cập nhật thành công cuốn sách: " + tens);
            getData();
        }
    }
}
