# How to use this package

1. Download this package somewhere, and add it as reference to the project
2. Create the related entities
3. Create the database context
4. Foreach entity, create a controller, extending Lturi.Api.Framework.ApiController

For usages, check your swagger for the newly created routes. 
Also to include more documentation, place the last below 2 lines in the AddSwaggerGen method body, for example like
>
    builder.Services.AddSwaggerGen(options =>
    {
        options.EnableAnnotations();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ToDo API",
        });

        List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
    });
This includes all the xml generated with the documentation in swagger, in each compiled project
Example (in this case the entity is "Student" and it's related DbContext is "EntityContext")
>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ApiController<Student>
    {
        EntityContext context;
        public StudentsController(EntityContext context): base(context)
        {
            this.context = context;
        }
    
        // GET: api/Students
        [HttpGet]
        public Response<IEnumerable<Student>> GetStudents()
        {
            return ListRequest(context.Students);
        }
    
        // GET: api/Students/search
        [HttpPost("search")]
        public Response<IEnumerable<Student>> SearchStudents(Request request)
        {
            return ListRequest(context.Students, request.Filters);
        }
    
        // GET: api/Students/5
        [HttpGet("{id}")]
        public Response<Student> GetStudent(int id)
        {
            return GetRequest(context.Students, id);
        }
    
        // PUT: api/Students/5
        [HttpPut("{id}")]
        public Response<Student> PutStudent(int id, Student student)
        {
            return PutRequest(id, student);
        }
    
        // POST: api/Students
        [HttpPost]
        public Response<Student> PostStudent(Student student)
        {
            return PostRequest(context.Students, student);
        }
    
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public Response<bool> DeleteStudent(int id)
        {
            return DeleteRequest(context.Students, id);
        }
    }
