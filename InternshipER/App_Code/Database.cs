using Npgsql;
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace InternshipER.App_Code
{
    public class Database
    {
        public static SqlConnection _productConn { get; private set; } //incele
        public static NpgsqlConnection connect()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["internshiper"].ConnectionString;
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            return con;
        }
        public static bool loginAttempt(String id, String password)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT count(*) FROM users WHERE (email = @Id OR username = @Id) AND password = @Password"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password));
                    cmd.Connection = con;
                    con.Open();
                    int count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if (count > 0) return true;
                    else return false;
                }
            }
        }
        internal static DataTable GetUserJob(int user_id)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select job_id, job_title, description, location, start_date, end_date, term from jobs inner join users on cast (jobs.user_id as bigint)=users.user_id where users.user_id=@UserId"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        internal static DataTable GetJobsAttendees(String jobId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select job_student.user_id, name, email, school, department from student_details inner join job_student on student_details.user_id = job_student.user_id where job_student.job_id=@jobId"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@jobId", jobId);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        internal static DataTable GetUserJobWithTitleAndLocation(int user_id)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select job_title, location, job_id from jobs inner join users on cast (jobs.user_id as bigint)=users.user_id where users.user_id=@UserId"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public static DataTable GetJob()
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select job_id, username, job_title, description, location, start_date, end_date, term from jobs inner join users on cast(jobs.user_id as bigint)=users.user_id"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
        public static DataTable GetAllUsers()
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select user_id, name, email, school, department from student_details"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }

        public static DataTable GetUserInter(string user)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select student_details.name, stud from interview  inner join  student_details on student_details.user_id=stud and comp like '" + user + "';"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }

        public static void createInterview(string stud, string comp)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO interview (comp, stud) VALUES(@Username, @Password)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", comp);
                    cmd.Parameters.AddWithValue("@Password", stud);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static DataTable GetAllUserInter(string user)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select job_student.user_id, name from student_details inner join job_student on student_details.user_id = job_student.user_id"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
        public static DataTable GetAllCompanies()
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select user_id, name, email, website, telephone,address from company_details"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
        public static DataTable GetFavorites(String StudentId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select company from favourites where student = @Student"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Student", StudentId);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
        public static int getUserId(String username, String password)
        {
            int user_id;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT user_id FROM users WHERE username = @Id AND password = @Password"))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", username);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password));
                    cmd.Connection = con;
                    con.Open();
                    user_id = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                }
            }
            return user_id;
        }
        public static DataTable GetMessage(string user)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select kime, kimden, mesaj from message where kime='" + user + "';"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }

        }
        public static void registerCompany(string companyName, String username, String password, String email)
        {
            int user_id;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO users (username, email, password) VALUES(@Username, @Email, @Password)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password));
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            user_id = getUserId(username, password);
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO company_details (user_id,name, email,address,telephone,website,description,title) VALUES(@User_id,@Name,@Email,@Address,@Telephone,@Website,@Description,@Title)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@User_id", user_id);
                    cmd.Parameters.AddWithValue("@Name", companyName);
                    cmd.Parameters.AddWithValue("@Email", "");
                    cmd.Parameters.AddWithValue("@Address", "");
                    cmd.Parameters.AddWithValue("@Telephone", "");
                    cmd.Parameters.AddWithValue("@Website", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@Title", "");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

        }
        public static void createMessage(string kime, string kimden, string mesaj)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO message (kime, kimden, mesaj) VALUES(@Username, @Email, @Password)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", kime);
                    cmd.Parameters.AddWithValue("@Password", mesaj);
                    cmd.Parameters.AddWithValue("@Email",kimden );
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void registerStudent(string studentName, String username, String password, String email)
        {
            int user_id;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO users (username, email, password) VALUES(@Username, @Email, @Password)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password));
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            user_id = getUserId(username, password);
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO student_details (user_id,name,email,adress,country,department,description,phone,website,age,school) VALUES(@User_id,@Name,@Email,@Address,@Country,@Department,@Description,@Phone,@Website,@Age,@School)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@User_id", user_id);
                    cmd.Parameters.AddWithValue("@Name", studentName);
                    cmd.Parameters.AddWithValue("@Email", "");
                    cmd.Parameters.AddWithValue("@Address", "");
                    cmd.Parameters.AddWithValue("@Country", "");
                    cmd.Parameters.AddWithValue("@Department", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@Phone", "");
                    cmd.Parameters.AddWithValue("@Website", "");
                    cmd.Parameters.AddWithValue("@Age", "");
                    cmd.Parameters.AddWithValue("@School", "");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

        }
        private static String Encrypt(string clearText)
        {
            string Key = "INTV1SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private static string Decrypt(string cipherText)
        {
            string Key = "INTV1SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static List<String> companyInfo(int userId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT description,name,email,address,telephone,website,title FROM company_details WHERE user_id = @Id"))
                {
                    List<string> infos = new List<string>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", userId.ToString());
                    cmd.Connection = con;
                    con.Open();
                    NpgsqlDataReader values = cmd.ExecuteReader();
                    while (values.Read())
                    {
                        infos.Add(values[0].ToString());
                        infos.Add(values[1].ToString());
                        infos.Add(values[2].ToString());
                        infos.Add(values[3].ToString());
                        infos.Add(values[4].ToString());
                        infos.Add(values[5].ToString());
                        infos.Add(values[6].ToString());
                    }
                    con.Close();
                    return infos;
                }
            }
        }
        public static String companyLocation(int i)
        {
            int temp = 0;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT location FROM jobs"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    NpgsqlDataReader values = cmd.ExecuteReader();
                    if (values.Read())
                    {
                        return values[i].ToString();

                    }
                    con.Close();
                    return null;
                }
            }
        }
        /*public static List<string> companyLocation()
        {
            int temp = 0;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT location FROM jobs"))
                {
                    List<string> locations = new List<string>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    NpgsqlDataReader values = cmd.ExecuteReader();
                    while (values.Read())
                    {
                        foreach (string prime in locations)
                        {
                            if (values[temp].ToString().Equals(prime))
                            {

                            }
                            else
                            {
                                locations.Add(values[temp].ToString());
                            }
                        }
                        temp++;
                    }
                    con.Close();
                    return locations;
                }
            }
        }*/
        public static void updateCompanyProfile(int userId, String companyName, String title, String website, String email, String description, String tel, String address)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE company_details SET title=@Title,description=@Desc,name=@Name,email=@Email,address=@Address,telephone=@Tel,website=@Website WHERE user_id = @Id", con))
                {
                    List<string> infos = new List<string>();
                    cmd.Parameters.AddWithValue("@Desc", description);
                    cmd.Parameters.AddWithValue("@Name", companyName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Tel", tel);
                    cmd.Parameters.AddWithValue("@Website", website);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Id", userId.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static List<String> studentInfo(string userId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT adress, age, country, department, description, email, name, phone, website, school FROM student_details WHERE user_id = @Id"))
                {
                    List<string> infos = new List<string>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", userId);
                    cmd.Connection = con;
                    con.Open();
                    NpgsqlDataReader values = cmd.ExecuteReader();
                    while (values.Read())
                        for (int i = 0; i < 10; i++)
                        {
                            if (!values.IsDBNull(i))
                                infos.Add(values.GetString(i));
                            else
                                infos.Add("");
                        }
                    con.Close();
                    return infos;
                }
            }
        }
        internal static void updateStudenProfile(string id, string adress, string age, string country, string department, string description, string email, string name, string phone, string website, string school)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE student_details SET address=@Adress, description=@Desc, name=@Name, email=@Email, age=@Age, phone=@Tel, website=@Website, country=@Country, department=@Department, school=@School WHERE user_id = @Id", con))
                {
                    List<string> infos = new List<string>();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Adress", adress);
                    cmd.Parameters.AddWithValue("@Desc", description);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Age", age.ToString());
                    cmd.Parameters.AddWithValue("@Tel", phone);
                    cmd.Parameters.AddWithValue("@Website", website);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@Department", department);
                    cmd.Parameters.AddWithValue("@School", school);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void createNewJob(String userId, String jobTitle, String jobDesc, String jobLocation, bool jobStatus)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO jobs (user_id, job_title, description,location,status,date) VALUES(@user_id, @job_title, @description,@location,@status,@date)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@job_title", jobTitle);
                    cmd.Parameters.AddWithValue("@description", jobDesc);
                    cmd.Parameters.AddWithValue("@location", jobLocation);
                    cmd.Parameters.AddWithValue("@status", jobStatus);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void organizeFavourite(String studentId, String companyId, Boolean flag)
        {
            String query;
            if (flag)
                query = "DELETE FROM favourites WHERE student=@Student and company=@Company";
            else
                query = "INSERT INTO favourites (student, company) VALUES(@Student, @Company)";


            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Student", studentId);
                    cmd.Parameters.AddWithValue("@Company", companyId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static bool favouriteCheck(String studentId, String companyId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT count(*) FROM favourites WHERE student=@Student and company=@Company"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Student", studentId);
                    cmd.Parameters.AddWithValue("@Company", companyId);
                    cmd.Connection = con;
                    con.Open();
                    int count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if (count > 0) return true;
                    else return false;
                }
            }
        }
        public static void jobAdd2User(string job_id, String user_id, string sop, DateTime date)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO job_student (job_id, user_id, sop ,date) VALUES(@job, @user, @sop, @date)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@job", job_id);
                    cmd.Parameters.AddWithValue("@user", user_id);
                    cmd.Parameters.AddWithValue("@sop", sop);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static bool isStudent(String user_id)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT count(*) FROM student_details WHERE user_id = @Id"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", user_id);
                    cmd.Connection = con;
                    con.Open();
                    int count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if (count > 0) return true;
                    else return false;
                }
            }
        }
        internal static void saveEvaluation(String reviewer, String target, String title, String message, int rate, String jobId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO review (target, reviewer, message , title, grade, date, job_id) VALUES(@Target, @Reviewer, @Message , @Title, @Grade, @Date, @JobId)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Target", target);
                    cmd.Parameters.AddWithValue("@Reviewer", reviewer);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@Grade", rate);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@JobId", jobId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        internal static DataTable GetAssignedJobs(String user_id)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select jobs.job_id, job_title, description, location, start_date, end_date, term from jobs inner join job_student on job_student.job_id=cast(jobs.job_id as text) where job_student.user_id=@UserId"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public static DataTable[] GetHomeStream()
        {
            DataTable[] tableList = new DataTable[2];
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select date,reviewer,target, title, message, grade from review ORDER BY date DESC LIMIT 10"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (dt)
                        {
                            sda.Fill(dt);

                        }
                    }
                }
            }
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select date,user_id, job_title, description,location from jobs ORDER BY date DESC LIMIT 10"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (dt2)
                        {
                            sda.Fill(dt2);

                        }
                    }
                }
            }
            tableList[0] = dt;
            tableList[1] = dt2;
            return tableList;

        }
        public static DataTable GetLastReviews(String user_id)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select date,reviewer,target, title, message, grade from review where target=@UserId ORDER BY date DESC LIMIT 2"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        List<string> infos = new List<string>();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (dt)
                        {
                            sda.Fill(dt);

                        }
                        return dt;
                    }
                }
            }

        }

        public static DataTable GetLastReviewsStudent(String user_id)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select date,reviewer,target, title, message, grade from review where target=@UserId ORDER BY date DESC LIMIT 2"))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        List<string> infos = new List<string>();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (dt)
                        {
                            sda.Fill(dt);

                        }
                        return dt;
                    }
                }
            }

        }

        public static void saveTest(string companyId, String testName, int questionNumber, int testTime, List<List<String>> questions, String html)
        {
            int test_no;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO test (company_id, test_name, question_number,time,html) VALUES(@Companyid, @Testname, @Questionnumber, @Testtime,@Html)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Companyid", companyId);
                    cmd.Parameters.AddWithValue("@Testname", testName);
                    cmd.Parameters.AddWithValue("@Questionnumber", questionNumber);
                    cmd.Parameters.AddWithValue("@Testtime", testTime);
                    cmd.Parameters.AddWithValue("@Html", html);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            test_no = getTestNo(companyId, testName);
            for (int i = 0; i < questions.Count; i++)
            {
                String test_id = test_no.ToString();
                int no = int.Parse(questions[i][0]);
                String type = questions[i][1];
                String question = questions[i][2];
                String[] choices = questions[i][3].Split('$');

                using (NpgsqlConnection con = connect())
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO test_questions (test_id,question,type,choices,no) VALUES(@Testid,@Question,@Type,@Choices,@No)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Testid", test_id);
                        cmd.Parameters.AddWithValue("@Question", question);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Choices", choices);
                        cmd.Parameters.AddWithValue("@No", no);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
        }
        public static int getTestNo(string companyId, String testName)
        {
            int user_id;
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT test_no FROM test WHERE company_id = @Id AND test_name = @Testname"))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", companyId);
                    cmd.Parameters.AddWithValue("@Testname", testName);
                    cmd.Connection = con;
                    con.Open();
                    user_id = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                }
            }
            return user_id;
        }
        public static DataTable getTests(String companyId)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select test_no,test_name,question_number,time from test where company_id=@companyId "))
                {
                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@companyId", companyId);
                        con.Open();
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public static String getTestContext(String test_no)
        {
            using (NpgsqlConnection con = connect())
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT html FROM test where test_no=@TestNo"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@TestNo", int.Parse(test_no));
                    con.Open();
                    NpgsqlDataReader values = cmd.ExecuteReader();
                    if (values.Read())
                    {
                        return values[0].ToString();

                    }
                    con.Close();
                    return null;
                }
            }
        }
    }
}