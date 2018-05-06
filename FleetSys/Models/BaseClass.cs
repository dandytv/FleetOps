using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Utilities.DAL;
using CCMS.ModelSector;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using FleetOps.App_Start;
using ModelSector;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using FleetSys.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;
namespace FleetOps.Models
{


	public class BaseClass : Controller
	{

		protected override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
			filterContext.Controller.TempData["ExcMessage"] = filterContext.Exception.Message;
		}
		public delegate void StartConnection();
		public delegate void ExecuteCommand();
		public delegate void CloseConnection();
		public SmtpClient client;
		public MailMessage msg;
		private CustomUserManager _UserManager;
		public static int GetIssNo
		{
			get
			{
				return 1;
			}
		}
		public static string UrlPrefix
		{
			get
			{
				return ConfigurationManager.AppSettings["urlPrefix"];
			}
		}
		public static string GetClaimsInfo(string type)
		{
			string Value = String.Empty;
			var Identity = ClaimsPrincipal.Current.Identities.First();
			if (type.ToLower() == "userid")
			{
				Value = Identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;
			}
			return Value;
		}

		public string GetUserId
		{
			get
			{
				//_UserManager = new CustomUserManager();
				return GetClaimsInfo("userid");
			}
		}

		public int GetAcqNo
		{
			get
			{
				return 1;
			}
		}
		public ExcelPackage CreateExcel(string[] Headers, List<string[]> Rows, string Title = "Report")
		{
			int colIndex = 1, rowIndex = 4;
			var pkg = PrepareExcelHeader(Title, Headers);
			var ws = pkg.Workbook.Worksheets[1];
			var cell = ws.Cells[rowIndex, colIndex];
			foreach (var rowVal in Rows)
			{
				foreach (var CellValue in rowVal)
				{
					cell = ws.Cells[rowIndex, colIndex];
					cell.Value = CellValue;
					cell.Merge = true;
					colIndex++;
				}
				colIndex = 1;
				rowIndex++;
			}
			ws.Cells[ws.Dimension.Address].AutoFitColumns();
			return pkg;
		}
		public static ExcelPackage PrepareExcelHeader(string heading, string[] colnames)
		{
			var ExcelPkg = new ExcelPackage();
			ExcelPkg.Workbook.Worksheets.Add("Account Info");
			ExcelWorksheet ws = ExcelPkg.Workbook.Worksheets[1];
			ws.Name = "Account Info";
			ws.Cells.Style.Font.Size = 11;
			ws.Cells.Style.Font.Name = "Calibri";
			ws.Cells[1, 1].Value = heading;
			ws.Cells[1, 1, 1, 20].Merge = true;
			ws.Cells[1, 1, 1, 20].Style.Font.Bold = true;
			ws.Cells[1, 1, 1, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
			ws.Cells[1, 1, 1, 20].Style.Fill.BackgroundColor.SetColor(Color.Gray);
			ws.Cells[1, 1, 1, 20].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
			//var fill = ws.Cells.Style.Fill;
			//fill.PatternType = ExcelFillStyle.Solid;
			//fill.BackgroundColor.SetColor(Color.Gray);
			var border = ws.Cells.Style.Border;
			border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
			int colIndex = 1, rowIndex = 3;
			var cell = ws.Cells[rowIndex, colIndex];
			foreach (var col in colnames)
			{
				cell = ws.Cells[rowIndex, colIndex];
				cell.Value = col;
				cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
				cell.Style.Font.Bold = true;
				cell.Style.Fill.BackgroundColor.SetColor(Color.Black);
				cell.Style.Font.Color.SetColor(Color.White);
				colIndex++;
			}




			return ExcelPkg;
		}
		public async static Task<MsgRetriever> GetMessageCode(int msgCd)
		{
			FleetDataEngine Engine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);

			try
			{
				await Engine.InitiateConnectionAsync();
				SqlParameter[] Parameters = new SqlParameter[2];
				SqlCommand cmd = new SqlCommand();
				Parameters[0] = new SqlParameter("@MsgCd", msgCd);
				Parameters[1] = new SqlParameter("@LangId", "EN");
				var Reader = await Engine.ExecuteCommandAsync("WebGetMsg", CommandType.StoredProcedure, Parameters);
				while (Reader.Read())
				{
					if (msgCd < 54999)
					{
						var tempData = new MsgRetriever
						{
							flag = Convert.ToInt32(Reader["Flag"]),
							desp = Convert.ToString(Reader["Descp"])

						};
						return tempData;
					}
					else
					{
						var tempData = new MsgRetriever
						{
							flag = Convert.ToInt32(Reader["Flag"]),
							desp = Convert.ToString(Reader["Descp"])

						};
						return tempData;
					}

				}

				return new MsgRetriever
				{
					flag = 1,
					desp = "***"
				};
			}
			finally
			{
				Engine.CloseConnection();
			}
		}
		public string getPartialPath(string dir, string name)
		{
			return String.Format("~/Views/Shared/PartialViews/{0}/{1}.cshtml", dir, name);
		}
		public async static Task<IEnumerable<SelectListItem>> WebGetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
			try
			{
				await objEngine.InitiateConnectionAsync();
				SqlParameter[] Parameters = new SqlParameter[5];
				SqlCommand cmd = new SqlCommand();
				Parameters[0] = new SqlParameter("@IssNo", 1);
				Parameters[1] = new SqlParameter("@RefType", String.IsNullOrEmpty(refType) ? (object)DBNull.Value : refType);
				Parameters[2] = String.IsNullOrEmpty(RefNo) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", RefNo);
				Parameters[3] = String.IsNullOrEmpty(RefInd) ? new SqlParameter("@RefInd", DBNull.Value) : new SqlParameter("@RefInd", RefInd);
				Parameters[4] = String.IsNullOrEmpty(RefId) ? new SqlParameter("@RefId", DBNull.Value) : new SqlParameter("@RefId", RefId);
				var getObjData = await objEngine.ExecuteCommandAsync("WebGetRefLib", CommandType.StoredProcedure, Parameters);
				var list = new List<SelectListItem>();
				if (PrependNull)
				{
					list.Add(new SelectListItem { Text = "", Value = "" });
				}
				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["Descp"]),
						Value = Convert.ToString(getObjData["RefCd"]),
						Selected = Convert.ToString(getObjData["RefCd"]) == "1" ? true : false
					});
				}

                if (list.Count > 0)
                    list.First().Selected = true;

				return list;
			}
			finally
			{
				objEngine.CloseConnection();
			}
		}

        public static async Task<IEnumerable<SelectListItem>> WebGetCycleStmt(string cycNo)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@CycNo", String.IsNullOrEmpty(cycNo) ? (object)DBNull.Value : cycNo);
                var getObjData = await objEngine.ExecuteCommandAsync("WebGetCycleStmt", CommandType.StoredProcedure, Parameters);
                var list = new List<SelectListItem>();
                while (getObjData.Read())
                {
                    list.Add(new SelectListItem { Text = Convert.ToDateTime(getObjData["StmtDate"]).ToString("yyyy-MM-dd"), Value = Convert.ToString(getObjData["CycStmtId"]) + ":" + Convert.ToDateTime(getObjData["StmtDate"]).ToString("yyyy-MM-dd") , Selected = Convert.ToString(getObjData["StmtDate"]) == "1" ? true : false });
                }
                return list;
            }catch(Exception ex)
            {
                System.Exception expt = new Exception("Error calls SP", ex);
                throw expt;
            }finally
            {
                objEngine.CloseConnection();
            }
        }
			
		


        public async Task<List<SelectListItem>> WebGetEvtInd()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                var getObjData = await objEngine.ExecuteCommandAsync("[WebGetEvtInd]", CommandType.StoredProcedure);
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                while (getObjData.Read())
                {
                    var item = new SelectListItem
                    {
                        Text = Convert.ToString(getObjData["Descp"]),
                        Value = Convert.ToString(getObjData["RefCd"]),
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }


		public static async Task<IEnumerable<SelectListItem>> iFrameGetTxnCategory()
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

			try
			{
				await objEngine.InitiateConnectionAsync();
				SqlParameter[] Parameters = new SqlParameter[1];
				SqlCommand cmd = new SqlCommand();
				var getObjData = await objEngine.ExecuteCommandAsync("iFrameGetTxnCategory", CommandType.StoredProcedure);

				var list = new List<SelectListItem>();

				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["Descp"]),
						Value = Convert.ToString(getObjData["Category"])
					});
				}
				if (list.Count > 0)
					list.First().Selected = true;
				return list;
			}
			finally
			{
				objEngine.CloseConnection();
			}

		}
		public async static Task<IEnumerable<SelectListItem>> WebGetAffiliate()
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
			try
			{
				await objEngine.InitiateConnectionAsync();
				SqlParameter[] Parameters = new SqlParameter[1];
				SqlCommand cmd = new SqlCommand();
				Parameters[0] = new SqlParameter("@IssNo", 1);
				var getObjData = await objEngine.ExecuteCommandAsync("WebGetAffiliate", CommandType.StoredProcedure, Parameters);
				var list = new List<SelectListItem>();
				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["Descp"]),
						Value = Convert.ToString(getObjData["Affiliate"])
					});
				}

				return list;
			}
			finally
			{
				objEngine.CloseConnection();
			}
		}
		public async static Task<IEnumerable<SelectListItem>> WebGetNonFleetTxnCode()
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
			try
			{
				await objEngine.InitiateConnectionAsync();
				SqlParameter[] Parameters = new SqlParameter[1];
				SqlCommand cmd = new SqlCommand();
				Parameters[0] = new SqlParameter("@IssNo", 1);


				var getObjData = await objEngine.ExecuteCommandAsync("WebGetNonFleetTxnCode", CommandType.StoredProcedure, Parameters);


				var list = new List<SelectListItem>();

				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["Descp"]),
						Value = Convert.ToString(getObjData["TxnCd"])


					});
				}
				return list;
			}
			finally
			{
				objEngine.CloseConnection();
			}
		}
		public async static Task<IEnumerable<SelectListItem>> WebUserAccessListSelect(string AccessInd = "I")
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);

			try
			{
				await objEngine.InitiateConnectionAsync();

				SqlParameter[] Parameters = new SqlParameter[2];
				SqlCommand cmd = new SqlCommand();
				Parameters[0] = new SqlParameter("@IssNo", "1");
				Parameters[1] = String.IsNullOrEmpty(AccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", AccessInd);

				var getObjData = await objEngine.ExecuteCommandAsync("WebUserAccessListSelect", CommandType.StoredProcedure, Parameters);

				var list = new List<SelectListItem>();
				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["UserId"]),
						Value = Convert.ToString(getObjData["UserId"])
					});

				}
				return list;
			}
			finally
			{
				objEngine.CloseConnection();
			}

		}

		public async static Task<IEnumerable<SelectListItem>> WebGetMap()
		{
			var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);


			try
			{
				await objEngine.InitiateConnectionAsync();

				SqlCommand cmd = new SqlCommand();
				var getObjData = await objEngine.ExecuteCommandAsync("WebGetMapId", CommandType.StoredProcedure);


				var list = new List<SelectListItem>();

				while (getObjData.Read())
				{
					list.Add(new SelectListItem
					{
						Text = Convert.ToString(getObjData["User Id"]),
						Value = Convert.ToString(getObjData["User Id"])
					});
				}
				return list;

			}
			finally
			{
				objEngine.CloseConnection();
			}
		}
		public static Boolean BoolConverter(object tempData) // YN to Bool
		{
			if (tempData != DBNull.Value)
			{
				if (string.IsNullOrEmpty(Convert.ToString(tempData)))
					return false;

				string opt = Convert.ToString(tempData);
				if ((opt == "No") || (opt == "N"))
				{
					return false;
				}
				else
					if ((opt == "Yes") || (opt == "Y"))
					{
						return true;
					}

			}
			else
			{
				return false;
			}


			return false;

		}
		public static string ConvertBoolDB(object tempObj) // Bool to YN
		{

			string DbValue;
			if (tempObj != null)
			{

				bool tempData = Convert.ToBoolean(tempObj);
				if (tempData == true)
				{
					return DbValue = "Y";
				}
				else
				{
					return DbValue = "N";
				}


			}
			else
			{
				return DbValue = Convert.ToString(DBNull.Value);
			}


			// return DbValue = "N";
		}

		public static int ConvertInt(object tempdata)
		{
			int temp = 0;
			if (tempdata != null && tempdata != DBNull.Value)
			{
				return temp = Convert.ToInt32(tempdata);
			}
			else
			{
				return temp;
			}


		}
		public static Object DateConverterDB(string tempData)//to database
		{
			DateTime tempDate;
			string datetime;
			if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
			{
				return DBNull.Value;
			}
			else
			{
				//tempDate = Convert.ToDateTime(Convert.ToDateTime(tempData).ToString("yyyy-MMMM-dd"));
				tempDate = DateTime.ParseExact(tempData, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				datetime = tempDate.ToString("yyyy-MMM-dd");
				return datetime;
			}
		}
		public static string DateConverter(object tempData)//from database
		{
			string tempDate;
			if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
			{
				tempDate = "";
			}
			else
			{
				tempDate = Convert.ToDateTime(tempData).ToString("dd/MM/yyyy");
				//tempDate = Convert.ToDateTime(tempData).ToString("yyyy-MMM-dd");
			}
			return tempDate;
		}
		public static string TimeConverter(object tempData)
		{
			DateTime tempDate;
			if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
			{
				return null;
			}
			else
			{
				tempDate = DateTime.ParseExact(Convert.ToString(tempData), "HH:mm", CultureInfo.InvariantCulture);
				return tempDate.ToString("HH:mm");
			}
		}
		public static Int64 ConvertToInt(object value)
		{
			if (value == DBNull.Value)
				return 0;
			return Convert.ToInt64(value);
		}
		public static Int32 ConvertToInt32(object value)
		{
			if (value == DBNull.Value)
				return 0;
			return Convert.ToInt32(value);
		}
		public static string DateTimeConverter(object tempData)//from database
		{
			string tempDate;
			if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
			{
				tempDate = "";
			}
			else
			{
				//tempDate = Convert.ToDateTime(tempData).ToString("hh:mm tt dd MMMM yyyy");
				//tempDate = Convert.ToDateTime(tempData).ToString("yyyy-MMM-dd");
				tempDate = Convert.ToDateTime(tempData).ToString("dd/MM/yyyy hh:mm tt");
			}
			return tempDate;
		}
		public static string DateTimeConverter2(object tempData)//from database
		{
			string tempDate;
			if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
			{
				tempDate = "";
			}
			else
			{
				tempDate = Convert.ToDateTime(tempData).ToString("hh:mm tt - dd MMM yyyy");
				//tempDate = Convert.ToDateTime(tempData).ToString("yyyy-MMM-dd");
				//tempDate = Convert.ToDateTime(tempData).ToString("dd/MMMM/yyyy HH:mm");
			}
			return tempDate;
		}
		public static string ConverterDecimal(object tempData)
		{
			decimal temp = Convert.ToDecimal("0.00");
			string test = "0.00";
			if ((tempData != DBNull.Value) && (tempData != null))
			{
				temp = Convert.ToDecimal(string.Format("{0:0.##}", tempData));
				return test = temp.ToString("#,##0.00");
			}
			else
			{
				return test;
			}
			//return temp;
		}
		public bool AutoGenerateFolder(int flag, string path)
		{
			if (flag == 0)
			{
				string fileRoot = path;
				if (!Directory.Exists(fileRoot))
				{
					try
					{
						Directory.CreateDirectory(fileRoot);
						return true;
					}
					catch (Exception)
					{
						return false;
					}
				}
			}
			return false;
		}
		public decimal ConvertDecimalToDb(object value)//to database
		{
			if (value != null && value != DBNull.Value)
			{
				return Convert.ToDecimal(value);
			}
			else
			{
				return 0;
			}
		}


		public int ConvertIntToDb(object value)//to database
		{
			if (value != null && value != DBNull.Value)
			{
				return Convert.ToInt16(value);
			}
			else
			{
				return 0;
			}
		}

		public Int64 ConvertLongToDb(object value)//to database
		{
			if (value != null && value != DBNull.Value)
			{
				return Convert.ToInt64(value);
			}
			else
			{
				return 0;
			}
		}

		public string ContentType(string tempType)
		{

			switch (tempType.ToLower())
			{

				case ".jpeg":
					return "image/jpeg";

				case ".jpg":
					return "image/jpeg";

				case ".gif":
					return "image/gif";

				case ".bmp":
					return "image/bmp";
				case ".png":
					return "image/png";
				case ".doc":
					return "application/msword";
				case ".docx":
					return "application/msword";

				case ".xls":
					return "application/vnd.ms-excel";
				case ".xlsx":
					return "application/vnd.ms-excel";
				case ".csv":
					return "application/csv";

				case ".pdf":
					return "application/pdf";

				case ".ppt":
					return "application/vnd.ms-powerpoint";
				case ".pptx":
					return "application/vnd.ms-powerpoint";

				case ".html":
					return "text/html";
				case ".htm":
					return "text/html";

				case ".stm":
					return "text/html";
				case ".tif":
					return "image/tiff";

				case ".tiff":
					return "image/tiff";

				default:
					return "application/octet-stream";

			}
		}
		public ImageFormat ImageType(string tempType)
		{

			switch (tempType.ToLower())
			{

				case ".jpeg":
					return ImageFormat.Jpeg;

				case ".jpg":
					return ImageFormat.Jpeg;

				case ".gif":
					return ImageFormat.Gif;

				case ".bmp":
					return ImageFormat.Bmp;

				case ".ico":
					return ImageFormat.Icon;

				default:
					return ImageFormat.Png;

			}
		}
		private string username()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]);

		}
		private string password()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]);

		}
		private string email()
		{
			return Convert.ToString(ConfigurationManager.AppSettings["EmailAddress"]);

		}

		public void SendEmail(string SendTo, string Subject, string body)
		{
			try
			{
				NetworkCredential smtpCreds = new NetworkCredential(email(), password());

				client.Host = "smtp.gmail.com";
				client.Port = 587;
				client.UseDefaultCredentials = false;
				client.Credentials = smtpCreds;
				client.EnableSsl = true;
				MailAddress to = new MailAddress(SendTo);
				MailAddress from = new MailAddress(username());
				msg.Subject = Subject;
				msg.Body = body;
				msg.From = from;
				msg.To.Add(to);
				client.Send(msg);
			}
			catch
			{
				Session["EmailError"] = "Email Failed To Send Out";

			}

		}
		public object GetInfo(DataTable dt)
		{
			var datarow = dt.AsEnumerable().ToList();
			var filtered = datarow.Take(2000);
			List<object> Rows = new List<object>();
			string[] columnNames = dt.Columns.Cast<DataColumn>()
								 .Select(x => x.ColumnName)
								 .ToArray();
			foreach (var x in datarow)
			{
				Rows.Add(x.ItemArray);
			}
			return new { columns = columnNames, rows = Rows };
		}
		public List<object> GetDataTableRows(DataTable dt)
		{
			var datarow = dt.AsEnumerable().ToList();
			List<object> Rows = new List<object>();
			foreach (var x in datarow)
			{
				Rows.Add(x.ItemArray);
			}
			return Rows;
		}
		public Byte[] ConvertListToExcelReport(DataTable Table, string Title)
		{
			var TableInfo = this.GetInfo(Table);
			Type myType = TableInfo.GetType();
			IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
			var columns = props[0].GetValue(TableInfo, null);
			var columns2 = (string[])columns;
			int colIndex = 1, rowIndex = 4;
			var pkg = PrepareExcelHeader(Title, columns2);
			var ws = pkg.Workbook.Worksheets[1];
			var cell = ws.Cells[rowIndex, colIndex];
			var getRows = this.GetDataTableRows(Table);
			Type rType = getRows.GetType();
			IList<PropertyInfo> props2 = new List<PropertyInfo>(rType.GetProperties());
			foreach (var x in getRows)
			{
				string[] arr = ((IEnumerable)x).Cast<object>()
								 .Select(y => y.ToString())
								 .ToArray();
				colIndex = 1;
				foreach (var rowVal in arr)
				{
					cell = ws.Cells[rowIndex, colIndex];
					cell.Value = rowVal;
					colIndex++;
				}
				rowIndex++;
			}
			Byte[] bin = pkg.GetAsByteArray();
			return bin;
		}
		public object downloadPath(string _filename, string _fileDirectory, string _fileExtension)
		{

			var Parent = Session["folderpath"].ToString();
			string file = _fileDirectory + "/" + _filename;
			string ext = Path.GetExtension(file);
			string TempFile;

			if (_fileDirectory.Contains(':'))
			{
				int tempObj = _fileDirectory.IndexOf(':');

				string tempPath = _fileDirectory.Substring(tempObj);
				TempFile = tempPath.Replace(':', '/');
			}
			else
			{
				TempFile = "";
			}

			return TempFile;

		}
		public string columnNames(DataTable dtSchemaTable, string delimiter)
		{
			string strOut = "";
			if (delimiter.ToLower() == "tab")
			{
				delimiter = "\t";
			}

			for (int i = 0; i < dtSchemaTable.Rows.Count; i++)
			{
				strOut += dtSchemaTable.Rows[i][0].ToString();
				if (i < dtSchemaTable.Rows.Count - 1)
				{
					strOut += delimiter;
				}

			}
			return strOut;
		}
		public string ToCSV(DataTable table)
		{
			var columnHeaders = (from DataColumn x in table.Columns
								 select x.ColumnName).ToArray();
			StringBuilder builder = new StringBuilder(String.Join(";", columnHeaders));
			builder.Append("\n");

			foreach (DataRow row in table.Rows)
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					builder.Append(row[i].ToString());
					builder.Append(i == table.Columns.Count - 1 ? "\n" : ";");
				}
			}

			return builder.ToString();
		}
		public List<DataTable> CloneTable(DataTable tableToClone, int countLimit)
		{
			List<DataTable> tables = new List<DataTable>();
			int count = 0;
			DataTable copyTable = null;
			foreach (DataRow dr in tableToClone.Rows)
			{
				if ((count++ % countLimit) == 0)
				{
					copyTable = new DataTable();
					// Clone the structure of the table.
					copyTable = tableToClone.Clone();
					// Add the new DataTable to the list.
					tables.Add(copyTable);
				}
				// Import the current row.
				copyTable.ImportRow(dr);
			}
			return tables;
		}
		static public bool IsTimeOfDayBetween(DateTime time,
									  TimeSpan startTime, TimeSpan endTime)
		{
			if (endTime == startTime)
			{
				return true;
			}
			else if (endTime < startTime)
			{
				return time.TimeOfDay <= endTime ||
					time.TimeOfDay >= startTime;
			}
			else
			{
				return time.TimeOfDay >= startTime &&
					time.TimeOfDay <= endTime;
			}

		}
		//public static DataTable ListToDataTable<T>(List<T> items) {

		//    DataTable dataTable = new DataTable(typeof(T).Name);



		//}

		//#region "SignalR"
		//public JsonResult DeleteTable(int value) 
		//{

		//    try
		//    {


		//        //Inform all connected clients
		//        var clientName = Request["clientName"];
		//        Task.Factory.StartNew(
		//            () =>
		//            {
		//                var clients = Hub<RealTimeJTable>();
		//                clients.RecordDeleted(clientName, studentId);
		//            });

		//        //Return result to current (caller) client
		//        return Json(new { Result = "OK" });
		//    }
		//    catch (Exception ex)
		//    {
		//        return Json(new { Result = "ERROR", Message = ex.Message });
		//    }


		//}
		//#endregion

		//public string YearMonth (object tempdate)
		//{
		//    string temp; 
		//    if (tempdate == "YYYY-MM-dd")
		//    {
		//       temp =

		//    }
		//}
		public static Object ConvertDatetimeDB(string tempData)//to database
		{
			string[] formats = {"dd/MM/yyyy h:m tt", "dd/MM/yyyy hh:mm tt","dd/MM/yyyy H:mm","dd/MM/yyyy HH:mm", "dd/MM/yyyy h:mm:ss tt","dd/MM/yyyy hh:mm:ss tt","dd/MM/yyyy H:mm:ss", "dd/MM/yyyy HH:mm:ss",
								"d/M/yyyy h:m tt", "d/M/yyyy hh:mm tt","d/M/yyyy H:mm","d/M/yyyy HH:mm", "d/M/yyyy h:mm:ss tt","d/M/yyyy hh:mm:ss tt","d/M/yyyy H:mm:ss", "d/M/yyyy HH:mm:ss", 
								"dd/MM/yyyy","d/M/yy h:m tt", "M/d/yy hh:mm tt","M/d/yy H:mm","M/d/yyyy HH:mm", "M/d/yy h:mm:ss tt","M/d/yy hh:mm:ss tt","M/d/yy H:mm:ss", "M/d/yy HH:mm:ss",
								"MM/dd/yy h:m tt", "MM/dd/yy hh:mm tt","MM/dd/yy H:mm","MM/dd/yy HH:mm", "MM/dd/yy h:mm:ss tt","MM/dd/yy hh:mm:ss tt","MM/dd/yy H:mm:ss", "MM/dd/yy HH:mm:ss",
								"dd/MM/yyyy", "d/M/yyyy", "M/d/yy", "MM/dd/yy","dd-MM-yyyy"};
			//string temp = Convert.ToString(tempData);
			DateTime OutDateTime;
			string datetime;
			if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
			{
				return DBNull.Value;
			}
			else
			{
				foreach (string i in formats)
				{
					if (DateTime.TryParseExact(tempData, i, new CultureInfo("en-US"), DateTimeStyles.None, out OutDateTime))
					{


						if (!i.ToLower().Contains("h"))
						{
							datetime = OutDateTime.ToString("yyyy-MMM-dd");


						}
						else
						{
							datetime = OutDateTime.ToString("yyyy-MMM-dd HH:mm:ss");

						}
						return datetime;
						//tempDate = DateTime.ParseExact(tempData, i, CultureInfo.InvariantCulture);

					}
					//tempDate = Convert.ToDateTime(tempData).ToString("yyyy-MMM-dd");

				}

			}

			return DateTime.MinValue;


		}

		public string GetPO
		{
			get
			{
				var temp = "PO";
				return temp;
			}
		}

		public string GetDA
		{
			get
			{
				var temp = "DA";
				return temp;
			}
		}

		public static void CopyValues<T>(T target, T source)
		{
			Type t = typeof(T);

			var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

			foreach (var prop in properties)
			{
				var value = prop.GetValue(source, null);
				if (value != null)
					prop.SetValue(target, value, null);
			}
		}

        /// <summary>
        /// Get Web User Access
        /// </summary>
        /// <returns></returns>
        public async static Task<IEnumerable<SelectListItem>> WebGetUserAccess(string userAccess,bool PrependNull=true)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", 1);
                Parameters[1] = new SqlParameter("@DropDown", String.IsNullOrEmpty(userAccess) ? (object)DBNull.Value : userAccess);

                var getObjData = await objEngine.ExecuteCommandAsync("WebDropDownInfo", CommandType.StoredProcedure, Parameters);
                var list = new List<SelectListItem>();
                if (PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }
                while (getObjData.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Text = Convert.ToString(getObjData["Name"]),
                        Value = Convert.ToString(getObjData["UserId"]),
                    });
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }


        }

	}

}


