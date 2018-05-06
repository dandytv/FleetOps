using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.IO;
using System.Data;
using System.Reflection;
using ModelSector.Global_Resources;
using CCMS.ModelSector;
using System.Xml.Linq;

namespace CCMS.ModelSector
{

    public class MsgRetriever
    {
        public int flag { get; set; }
        public string desp { get; set; }
        public string Id { get; set; }
    }
    public class Home
    {
        public string Controller { get; set; }
        public string IconFile { get; set; }
        public string PageName { get; set; }
        public string Locale { get; set; }
        public string Action { get; set; }
    }
    public class JqGrid
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public object rows { get; set; }
    }
    public class JqString
    {
        public int page { get; set; }
        public int rows { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public bool _search { get; set; }
        public string searchField { get; set; }
        public string searchString { get; set; }
        public string searchOper { get; set; }
    }

    public class Accessibility
    {
        public string CtrlId { get; set; }
        public int Sts { get; set; }
        public string SectionName { get; set; }
        public string Page { get; set; }
        public string SectionId { get; set; }
        public string SectionShortCd { get; set; }
        public int Sequence { get; set; }
    }

    public class Section
    {
        public Int32 SectionId { get; set; }
    }


    public class VisualSettings
    {
        public string Theme { get; set; }
        public string Layout { get; set; }
        public string FormDisplayMode { get; set; }
        public string Animation { get; set; }
        public string Background { get; set; }
        public int Opacity { get; set; }
        public string TopHex { get; set; }
    }
    public class DirHierarchy
    {
        public string title { get; set; }
        public bool isFolder { get; set; }
        public string key { get; set; }
        public List<DirHierarchy> children { get; set; }
    }


    public class GoogleVisualizationTableCols
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
    }

    public class GoogleRowWrapper
    {
        public List<googleVisualCell> c { get; set; }
        public object p;

    }

    public class googleVisualCell
    {
        public string v { get; set; }
        public string f { get; set; }

    }

    public class fileManagerFiles
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string LastModified { get; set; }
        public string CreatedDate { get; set; }
        public string Size { get; set; }
    }

    public class Themes
    {
        public string ThemeID { get; set; }
        public string ThemeName { get; set; }
    }

    public class autoCompleteModel
    {
        public string maxRows { get; set; }
        public string name_startsWith { get; set; }
    }

    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    /// 
    public class JqueryUIAutoCompleteModel
    {
        public string featureClass { get; set; }
        public int maxRows { get; set; }
        public string name_startsWith { get; set; }
        public string style { get; set; }

    }

    public class jQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

    }


    public interface IGeneral
    {
        int totalRecords { get; set; }
        int displayRecods { get; set; }
        string DownloadFile(string path);
        List<Home> SearchString(jQueryDataTableParamModel Model);
    }

    public class CardnAccNo
    {
        [DisplayName("Account No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public String AccNo { get; set; }
        [DisplayName("Card No")]
        //[RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayName("New Card No")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string NewCardNo { get; set; }
    }


    public class General
    {

        public string DownloadFile(string path)
        {
            return "x";
        }
        public static IQueryable<T> Filterfurther<T>(JqString param, IQueryable<T> exp)
        {
            if (param._search)
            {
                PropertyInfo[] props = typeof(T).GetProperties();
                var typeofit = props.Where(p => p.Name.Equals(param.searchField)).Select(p => p.PropertyType).FirstOrDefault();
                object o2 = Convert.ChangeType(param.searchString, typeofit);
                Type t = o2.GetType();
                ParameterExpression pe = Expression.Parameter(typeof(T), param.searchField);
                Expression id = Expression.PropertyOrField(pe, param.searchField);
                Expression two = Expression.Constant(o2, t);
                Expression e1 = Expression.Equal(id, two);
                MethodCallExpression whereCallExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { exp.ElementType },
                    exp.Expression, Expression.Lambda<Func<T, bool>>(e1, new ParameterExpression[] { pe }));
                var results = exp.Provider.CreateQuery<T>(whereCallExpression);
                List<T> vals = results.ToList();
                return results.AsQueryable();
            }
            return exp;
        }
        public int totalRecords
        {
            get;
            set;
        }

        public int displayRecods
        {
            get;
            set;
        }

    }

    public class FilesStatus
    {
        public const string HandlerPath = "/Upload/";

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }

        public FilesStatus() { }

        public FilesStatus(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName); }

        public FilesStatus(string fileName, int fileLength, string fullPath) { SetValues(fileName, fileLength, fullPath); }

        private void SetValues(string fileName, int fileLength, string fullPath)
        {
            name = fileName;
            type = "image/png";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_type = "DELETE";

            var ext = Path.GetExtension(fullPath);

            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (fileSize > 3 || !IsImage(ext)) thumbnail_url = "/Content/img/generalFile.png";
            else thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpg" || ext == ".png";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}



