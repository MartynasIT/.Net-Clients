
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace clients
{
    public partial class ClientWindow : Form
    {
        private SqlConnection connection;
        private string API_KEY = "";
        private string DB_ERROR = "Problem with database";
        private string API_ERROR = "Problem with api call";
        private String DATABASE = "Data Source=DESKTOP-9HN26R3\\MSSQLSERVER1;Initial Catalog=master;Integrated Security=True";
        public ClientWindow()
        {
            InitializeComponent();
            
        }


        public void set_api()
        {
            this.API_KEY = Properties.Settings.Default.api_saved;
          
        }

        public string get_db()
        {
            return Properties.Settings.Default.database_saved;

        }


        public void set_connection(SqlConnection connection)
        {
            this.connection = connection;
        }

        public SqlConnection get_connection()
        {
            return connection;
        }

        public void open_connection()
        {
            SqlConnection connection = new SqlConnection(DATABASE);
            connection.Open();
            this.connection =  connection;
        }

        public string get_api()
        {
            return API_KEY;
        }

        private void log_into_db(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, get_connection());
                cmd.ExecuteNonQuery();
               
            }
            catch(Exception exmp)
            {
                MessageBox.Show("Logging error");
            }
       
        }

        private void store_data_Click(object sender, EventArgs e)
        {

            try
            {

            open_connection();
            if (get_connection().State == System.Data.ConnectionState.Open)
            {
                set_connection(connection);
          
             
                var json_string = File.ReadAllText("klientai.json", Encoding.UTF8);
                Debug.WriteLine(json_string);
                JArray items = JArray.Parse(json_string);
                foreach (JObject item in items)
                {
                    string name = item.GetValue("Name").ToString();
                    string address = item.GetValue("Address").ToString();
                    String post_code = " ";
                    if (item.GetValue("PostCode") != null)
                    {
                        post_code = item.GetValue("PostCode").ToString();
                    }
                    put_data(name, address, post_code);
                }

                MessageBox.Show("Data inserted");

            }
            }
            catch (Exception exmp)
            {
                MessageBox.Show("Server error");
            }
        }

        private void put_data(params string[] args)
        {
            if (check_if_exists(args[0]) == true)
            {
                MessageBox.Show(args[0] + " already exists in database");
                System.Windows.Forms.Application.ExitThread();

            }
            else
            {
                try
                {
                    string querry = "INSERT INTO dbo.Clients (name, address, post_code) VALUES (N'" + args[0] + "', N'" + args[1] + "',N'" + args[2] + "');";
                    string log = "INSERT INTO dbo.Logs (action, record) VALUES ('created', N'" + args[0] + "');";
                    Debug.WriteLine(querry.ToString());
                    SqlCommand cmd = new SqlCommand(querry, get_connection());
                    cmd.ExecuteNonQuery();
                    log_into_db(log);
                }
                catch (Exception exmp)
                {
                    MessageBox.Show("Server error");
                }

            }
  
        }

        private Boolean check_if_exists(string name)
        {
            try
            {
                string querry = "SELECT COUNT(*) FROM dbo.Clients WHERE name = N'" + name + "';";
                SqlCommand cmd = new SqlCommand(querry, get_connection());
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                    return true;

                else
                    return false;
            }

            catch (Exception exmp)
            {
                MessageBox.Show("Server error");
                return true;
            }

        }

        
        private String form_url(string address)
        {
            string street = "";
            string number = "";
            string city = "";
            int index = address.IndexOf("g.");
            if (index > 0)
            {
                street = address.Substring(0, index);

            }
            else
            {
                index = address.IndexOf("al.");
                if (index > 0)
                {
                    street = address.Substring(0, index);
                }
       
                else
                {
                    index = address.IndexOf("pr.");
                    street = address.Substring(0, index);
                }
            }
            city = address.Substring(address.LastIndexOf(',') + 1);

            int from = address.IndexOf(".") + ".".Length;
            int to = address.LastIndexOf(",");
            number = address.Substring(from, to - from);
            int index_number = number.IndexOf(",");
            if (index_number > 0)
            {
                city = number.Substring(number.LastIndexOf(',') + 1);
                number = number.Substring(0, index_number);
                

            }
            
            string url = String.Format("https://api.postit.lt/?term={0}+{1},+{2}&key={3}", street, number, city, get_api()).Replace(" ", string.Empty);
            Debug.WriteLine(url);
            return url;
        }


        private void update_posts_Click(object sender, EventArgs e)
        {
            set_api();
            open_connection();
            SqlCommand cmd = new SqlCommand("select * from dbo.Clients", get_connection());
            SqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    {
                        string address = reader.GetString(reader.GetOrdinal("address")).ToString();
                        int id = reader.GetInt32(reader.GetOrdinal("id"));

                        string post_code = get_post_code(form_url(address));
                        update_post(id, post_code);

                    };

                }
                reader.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(DB_ERROR);
            }
      
        }


        private void update_post(int id, string post_code)
        {  
            string update_statement = @"UPDATE dbo.Clients SET post_code = @post Where id = @id";
            try
            {
                using (SqlConnection conn = new SqlConnection(DATABASE))
                using (SqlCommand cmd = new SqlCommand(update_statement, conn))
                {
                    cmd.Parameters.AddWithValue("@post", post_code);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
              
            }
            catch (Exception exp)
            {
                MessageBox.Show(DB_ERROR);
            }
        }

          private string get_post_code(string url)
          {
       
             Uri address = new Uri(url);
             HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
             request.Method = "GET";
             request.ContentType = "text/json";

             using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
             {
                 StreamReader reader = new StreamReader(response.GetResponseStream());

                try
                {
                    string api_output = reader.ReadToEnd();
                    var data = JObject.Parse(api_output)["data"];
                    Debug.WriteLine(data);
                    JToken post_code = data.First["post_code"];
                    return post_code.ToString();
                    
                }

                catch (Exception exp)
                {
                    MessageBox.Show(API_ERROR);
                    return "error";
                }

            }
       
        }

        private void clients_button_Click(object sender, EventArgs e)
        {
            data_window form = new data_window();
            form.ShowDialog(); 
        }

        private void Configuration_Click(object sender, EventArgs e)
        {
            configuration form = new configuration();
            form.ShowDialog();
            
        }
    }
}

