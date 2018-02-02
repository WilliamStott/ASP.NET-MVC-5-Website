
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

/**
 * Simple dataclass (model) containing the table layout
 **/
[Table("courses")]
public class CourseInfo
{
    [Key]
    [Column(Order = 1)]
    public int courseID { get; set; }

    public string courseName { get; set; }

    public int courseCredits { get; set; }

    public int courseDuration { get; set; }

    public string courseTutor { get; set; }

}

public class CourseInfoContext : DbContext
{
    public DbSet<CourseInfo> Courses { get; set; }
}
