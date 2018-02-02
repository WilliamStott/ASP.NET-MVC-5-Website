using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;


public class HomeController : Controller
{

    private DataBase db = new DataBase();
    private Converter cv = new Converter();

    public ActionResult Index()
    {
        //System.Diagnostics.Debug.WriteLine("I am here");
        return View();
    }


    [HttpPost]
    public async Task<bool> addCourse(CourseInfo ci)
    {
        try
        {
            if (ci.courseCredits != 0 & ci.courseDuration != 0 && ci.courseName != "" && ci.courseTutor != "")
            {
                return await db.addCourse(ci);
            }
            else
            {
                return false;
            }
        }
        catch(System.Exception e)
        {
            throw e;
        }
    }

    [HttpGet]
    public async Task<JsonResult> searchCourse(string returnType, string columnName, string searchTerm)
    {
        try
        {
            //Data validation to ensure that the column is one of the allowed
            if (new string[] { "All", "courseID", "courseName", "courseCredits", "courseDuration", "courseTutor" }.Contains(columnName))
            {
                switch (returnType)
                {
                    case "Default":
                        return Json(Converter.convertToHTML(await db.searchCourses(columnName, searchTerm)), JsonRequestBehavior.AllowGet);
                    case "JSON":
                        return Json(Converter.convertToJSON(await db.searchCourses(columnName, searchTerm)), JsonRequestBehavior.AllowGet);
                    case "XML":
                        return Json(Converter.convertToXML(await db.searchCourses(columnName, searchTerm)), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        catch(System.Exception e)
        {
            throw e;
        }
    }


    [HttpGet]
    //Asynchronous function
    public async Task<JsonResult> listCourse(string returnType, string columnName)
    {
        try
        {
            //Checks to see if the dataType has the allowed column names (SQL INJECTION PREVENTION)
            if (new string[] { "All", "courseID", "courseName", "courseCredits", "courseDuration", "courseTutor" }.Contains(columnName))
            {
                switch (returnType)
                {
                    //Awaits for each call to the database
                    case "Default":
                        return Json(Converter.convertToHTML(await db.listCourses(columnName)), JsonRequestBehavior.AllowGet);
                    case "JSON":
                        return Json(Converter.convertToJSON(await db.listCourses(columnName)), JsonRequestBehavior.AllowGet);
                    case "XML":
                        return Json(Converter.convertToXML(await db.listCourses(columnName)), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(Converter.convertToHTML(await db.listCourses("All")), JsonRequestBehavior.AllowGet);
        }
        catch(System.Exception e)
        {
            throw e;
        }
    }
}
