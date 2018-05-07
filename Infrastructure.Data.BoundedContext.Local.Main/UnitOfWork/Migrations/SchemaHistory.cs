//-----------------------------------------------------------------------
// <Copyright file="CraftBox.EReceipt.Infrastructure.Data.BoundedContext.Main.UnitOfWork.Initializer.SchemaHistory.cs" Company="CraftBox Asia">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2018-Feb-07 1:53:25 AM</Date>

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an 'AS IS' BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </Copyright>
//-----------------------------------------------------------------------


using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezRich.Infrastructure.Data.BoundedContext.Local.Main.UnitOfWork.Migrations
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2018-Feb-07 1:53:25 AM</para>
    /// <para>Usage: SchemaHistory is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class SchemaHistory : IHistory
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Context { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
