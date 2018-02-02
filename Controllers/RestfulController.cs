using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Assignment.Models;

public class RestfulController : ApiController
{
    private RestfulContext rc = new RestfulContext();
    private DataBase db = new DataBase();

    // GET: api/Restful
    public IQueryable<CourseInfo> GetCourseInfoes()
    {
        return rc.CourseInfoes;
    }

    // GET: api/Restful/5
    [ResponseType(typeof(CourseInfo))]
    public IHttpActionResult GetCourseInfo(int id)
    {
        CourseInfo courseInfo = rc.CourseInfoes.Find(id);
        if (courseInfo == null)
        {
            return NotFound();
        }

        return Ok(courseInfo);
    }

    // PUT: api/Restful/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutCourseInfo(int id, CourseInfo courseInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != courseInfo.courseID)
        {
            return BadRequest();
        }

        rc.Entry(courseInfo).State = EntityState.Modified;

        try
        {
            rc.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CourseInfoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/Restful
    [ResponseType(typeof(CourseInfo))]
    public IHttpActionResult PostCourseInfo(CourseInfo courseInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        rc.CourseInfoes.Add(courseInfo);
        rc.SaveChanges();

        return CreatedAtRoute("DefaultApi", new { id = courseInfo.courseID }, courseInfo);
    }

    // DELETE: api/Restful/5
    [ResponseType(typeof(CourseInfo))]
    public IHttpActionResult DeleteCourseInfo(int id)
    {
        try
        {
            CourseInfo courseInfo = rc.CourseInfoes.Find(id);
            if (courseInfo == null)
            {
                return NotFound();
            }

            rc.CourseInfoes.Remove(courseInfo);
            rc.SaveChanges();

            return Ok(courseInfo);
        }
        catch(System.Exception e)
        {
            throw e;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            rc.Dispose();
        }
        base.Dispose(disposing);
    }

    private bool CourseInfoExists(int id)
    {
        return rc.CourseInfoes.Count(e => e.courseID == id) > 0;
    }
}
