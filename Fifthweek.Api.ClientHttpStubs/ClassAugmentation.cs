using System;
using System.Linq;

//// Generated on 05/02/2015 19:57:23 (UTC)
//// Mapped solution in 3.39s


namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class ApiGraph 
    {
        public ApiGraph(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.ClientHttpStubs.ControllerElement> controllers)
        {
            if (controllers == null)
            {
                throw new ArgumentNullException("controllers");
            }

            this.Controllers = controllers;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ControllerElement 
    {
        public ControllerElement(
            System.String name,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.ClientHttpStubs.MethodElement> methods)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (methods == null)
            {
                throw new ArgumentNullException("methods");
            }

            this.Name = name;
            this.Methods = methods;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class MethodElement 
    {
        public MethodElement(
            Fifthweek.Api.ClientHttpStubs.HttpMethod httpMethod,
            System.String name,
            System.String route,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.ClientHttpStubs.ParameterElement> urlParameters,
            Fifthweek.Api.ClientHttpStubs.ParameterElement bodyParameter)
        {
            if (httpMethod == null)
            {
                throw new ArgumentNullException("httpMethod");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (route == null)
            {
                throw new ArgumentNullException("route");
            }

            if (urlParameters == null)
            {
                throw new ArgumentNullException("urlParameters");
            }

            this.HttpMethod = httpMethod;
            this.Name = name;
            this.Route = route;
            this.UrlParameters = urlParameters;
            this.BodyParameter = bodyParameter;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ParameterElement 
    {
        public ParameterElement(
            System.String name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs.Templates
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Microsoft.VisualStudio.TextTemplating;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Humanizer;

    public partial class AngularTemplate
    {
        public partial class RenderingSession 
        {
            public RenderingSession(
                Fifthweek.Api.ClientHttpStubs.ApiGraph api,
                Microsoft.VisualStudio.TextTemplating.TextTransformation output)
            {
                if (api == null)
                {
                    throw new ArgumentNullException("api");
                }

                if (output == null)
                {
                    throw new ArgumentNullException("output");
                }

                this.api = api;
                this.output = output;
            }
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs.Reflection
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http;
    using Fifthweek.CodeGeneration;

    public partial class ControllerSource 
    {
        public ControllerSource(
            System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }

            this.assemblies = assemblies;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.ClientHttpStubs.Reflection;
    using Fifthweek.Api.ClientHttpStubs.Templates;
    using Fifthweek.CodeGeneration;
    using Microsoft.VisualStudio.TextTemplating;

    public partial class StubFile 
    {
        public StubFile(
            Microsoft.VisualStudio.TextTemplating.TextTransformation output,
            Fifthweek.Api.ClientHttpStubs.Templates.ITemplate template)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            this.output = output;
            this.template = template;
        }
    }
}

namespace Fifthweek.Api.ClientHttpStubs
{
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using System;
    using System.Linq;

    public partial class ApiGraph 
    {
        public override string ToString()
        {
            return string.Format("ApiGraph({0})", this.Controllers == null ? "null" : this.Controllers.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((ApiGraph)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Controllers != null 
        			? this.Controllers.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ApiGraph other)
        {
            if (this.Controllers != null && other.Controllers != null)
            {
                if (!this.Controllers.SequenceEqual(other.Controllers))
                {
                    return false;    
                }
            }
            else if (this.Controllers != null || other.Controllers != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ControllerElement 
    {
        public override string ToString()
        {
            return string.Format("ControllerElement(\"{0}\", {1})", this.Name == null ? "null" : this.Name.ToString(), this.Methods == null ? "null" : this.Methods.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((ControllerElement)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Methods != null 
        			? this.Methods.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ControllerElement other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (this.Methods != null && other.Methods != null)
            {
                if (!this.Methods.SequenceEqual(other.Methods))
                {
                    return false;    
                }
            }
            else if (this.Methods != null || other.Methods != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class MethodElement 
    {
        public override string ToString()
        {
            return string.Format("MethodElement({0}, \"{1}\", \"{2}\", {3}, {4})", this.HttpMethod == null ? "null" : this.HttpMethod.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Route == null ? "null" : this.Route.ToString(), this.UrlParameters == null ? "null" : this.UrlParameters.ToString(), this.BodyParameter == null ? "null" : this.BodyParameter.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((MethodElement)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.HttpMethod != null ? this.HttpMethod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Route != null ? this.Route.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UrlParameters != null 
        			? this.UrlParameters.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.BodyParameter != null ? this.BodyParameter.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(MethodElement other)
        {
            if (!object.Equals(this.HttpMethod, other.HttpMethod))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Route, other.Route))
            {
                return false;
            }
        
            if (this.UrlParameters != null && other.UrlParameters != null)
            {
                if (!this.UrlParameters.SequenceEqual(other.UrlParameters))
                {
                    return false;    
                }
            }
            else if (this.UrlParameters != null || other.UrlParameters != null)
            {
                return false;
            }
        
            if (!object.Equals(this.BodyParameter, other.BodyParameter))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.ClientHttpStubs
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;

    public partial class ParameterElement 
    {
        public override string ToString()
        {
            return string.Format("ParameterElement(\"{0}\")", this.Name == null ? "null" : this.Name.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((ParameterElement)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ParameterElement other)
        {
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            return true;
        }
    }
}


