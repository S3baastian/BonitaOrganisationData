using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitaOrganisationData
{
    class Program
    {
        //string insert = "INSERT INTO ARista.UsuariosBonita(IdUsuarioBonita,NombreUsuario,Contraseña,ConfirmarContraseña,Avatar,Nombre,Apellido,Titulo,Profesion,IdResponsableBonitaArista,DireccionEmpresa,PaisEmpresa,CodigoPostalEmpresa,EstadoEmpresa,CorreoElectronicoEmpresa,TelefonoEmpresa,MovilEmpresa,DireccionPersonal,PaisPersonal,CodigoPostalPersonal,CorreoElectronicoPersonal,EstadoPersonal,TelefonoPersonal,MovilPersonal"
        //                + "VALUES (@id, @userName, @password, @password, @icon, @firstName, @lastName, @title, @jobTitle, @managerId, @address, @country, @zipCode, @state, @email, @phoneNumber, @mobileNumber, @addressPersonal, @countryPersonal, @zipCodePersonal, @emailPersonal, @statePersonal, @phoneNumberPersonal, @mobileNumberPersonal)";
        public static string getParentId(Dictionary<string, string> parentInfo, string parentName)
        {
            parentName = parentName.Substring(parentName.LastIndexOf('/') + 1);

            //var ds = MySqlConnector.select("select displayName, id from bonita.group_ where displayName like '" + parentName + "';");
            //parentInfo = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());           
            return parentInfo[parentName];
        }

        public static DateTime? ConvertUnixTimeStamp(string unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(unixTimeStamp));
        }
        
        public static void getRoleInformation()
        {
            var roles = MySqlConnector.select("SELECT id, name, displayName, description, iconid, createdBy, creationDate FROM bonita.role;");
            RoleInformation role;
            List<RoleInformation> rolesList = new List<RoleInformation>();
            string existingRoles = "";

            foreach (DataRow dr in roles.Tables[0].Rows)
            {
                role = new RoleInformation();
                role.id = dr[0].ToString();
                role.name = dr[1].ToString();
                role.dinamicName = dr[2].ToString();
                role.description = dr[3].ToString();
                role.icon = dr[4].ToString();               
                role.assignedBy = Convert.ToInt32(dr[5].ToString());
                role.creationDate = ConvertUnixTimeStamp(dr[6].ToString());
                role.active = 1;

                rolesList.Add(role);
                existingRoles += "'" + role.id + "', ";               
            }

            foreach (RoleInformation r in rolesList)
            {
                if (MySqlConnector.checkIfExists(r.id, "SELECT * FROM ARista.RolesBonita WHERE IdRolBonita = @id;"))
                {
                    MySqlConnector.insertRoles(r, "update");
                }
                else
                {
                    MySqlConnector.insertRoles(r, "insert");
                }
            }

            existingRoles = existingRoles.Substring(0, existingRoles.Length - 2);
            MySqlConnector.update("UPDATE ARista.RolesBonita SET IdEstadoActividad = " + 2 + " WHERE IdRolBonita NOT IN (" + existingRoles + ")");
        }

        public static void getUserInformation()
        {
            var users = MySqlConnector.select("select * from bonita.userInformation");
            List<UserInformation> usersList = new List<UserInformation>();
            UserInformation user;
            string existingUsers = "";

            foreach (DataRow dr in users.Tables[0].Rows)
            {
                user = new UserInformation();
                user.id = dr[0].ToString();
                user.userName = dr[1].ToString();
                user.password = dr[2].ToString();
                user.icon = dr[3].ToString();
                user.firstName = dr[4].ToString();
                user.lastName = dr[5].ToString();
                user.title = dr[6].ToString();
                user.jobTitle = dr[7].ToString();
                user.managerId = dr[8].ToString();
                user.address = dr[9].ToString();
                user.country = dr[10].ToString();
                user.zipCode = dr[11].ToString();
                user.state = dr[12].ToString();
                user.email = dr[13].ToString();
                user.phoneNumber = dr[14].ToString();
                user.mobileNumber = dr[15].ToString();
                user.addressPersonal = dr[16].ToString();
                user.countryPersonal = dr[17].ToString();
                user.zipCodePersonal = dr[18].ToString();
                user.statePersonal = dr[19].ToString();
                user.emailPersonal = dr[20].ToString();
                user.phoneNumberPersonal = dr[21].ToString();
                user.mobileNumberPersonal = dr[22].ToString();

                if (dr[23].ToString().Equals("True")) user.enabled = 1;
                else user.enabled = 2;

                user.assignedBy = Convert.ToInt32(dr[24].ToString());
                user.creationDate = ConvertUnixTimeStamp(dr[25].ToString());
                user.updateDate = ConvertUnixTimeStamp(dr[26].ToString());


                usersList.Add(user);
                existingUsers += "'" + user.id + "', ";
            }

            foreach (UserInformation u in usersList)
            {
                if (MySqlConnector.checkIfExists(u.id, "SELECT * FROM ARista.UsuariosBonita WHERE IdUsuarioBonita = @id;"))
                {
                    MySqlConnector.insertUsers(u, "update");
                }
                else
                {
                    MySqlConnector.insertUsers(u, "insert");
                }
            }

            existingUsers = existingUsers.Substring(0, existingUsers.Length - 2);
            MySqlConnector.update("UPDATE ARista.UsuariosBonita SET IdEstadoActividad = " + 2 + " WHERE IdUsuarioBonita NOT IN (" + existingUsers + ")");
        }

        public static void getMembershipInformation()
        {
            string existingMembers = "";
            List<MembershipInformation> membersList = new List<MembershipInformation>();
            MembershipInformation member;
            var members = MySqlConnector.select("SELECT id, userId, roleId, groupId, assignedBy, assignedDate FROM bonita.user_membership;");
            

            Dictionary<string, string> groupInfo = new Dictionary<string, string>();
            var ds = MySqlConnector.selectFromArista("SELECT IdGrupoBonita, IdGrupoBonitaArista FROM ARista.GruposBonita;");
            groupInfo = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());
            groupInfo.Add("", "");
            
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            ds = MySqlConnector.selectFromArista("SELECT IdUsuarioBonita, IdUsuarioBonitaArista FROM ARista.UsuariosBonita;");
            userInfo = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());
            userInfo.Add("", "");

            Dictionary<string, string> roleInfo = new Dictionary<string, string>();
            ds = MySqlConnector.selectFromArista("SELECT IdRolBonita, IdRolBonitaArista FROM ARista.RolesBonita;");
            roleInfo = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());
            roleInfo.Add("", "");

            foreach (DataRow dr in members.Tables[0].Rows)
            {
                member = new MembershipInformation();

                member.id = dr[0].ToString();
                member.userId = Convert.ToInt32(userInfo[dr[1].ToString()]);
                member.roleId = Convert.ToInt32(roleInfo[dr[2].ToString()]);
                member.groupId = Convert.ToInt32(groupInfo[dr[3].ToString()]);
                member.assignedBy = Convert.ToInt32(dr[4].ToString());
                member.assignedDate = ConvertUnixTimeStamp(dr[5].ToString());

                membersList.Add(member);
                existingMembers += existingMembers += "'" + member.id + "', ";
            }

            foreach (MembershipInformation m in membersList)
            {
                if (MySqlConnector.checkIfExists(m.id, "SELECT * FROM ARista.MembresiasBonitaRel where IdMembresiaBonita = @id;"))
                {
                    m.active = 1;
                    MySqlConnector.insertMembership(m, "update");
                }
                else
                {
                    m.active = 1;
                    MySqlConnector.insertMembership(m, "insert");
                }
            }

            existingMembers = existingMembers.Substring(0, existingMembers.Length - 2);
            MySqlConnector.update("UPDATE ARista.GruposBonita SET IdEstadoActividad = " + 2 + " WHERE IdMembresiaBonita NOT IN (" + existingMembers + ")");

        }

        public static void getGroupInformation()
        {
            Dictionary<string, string> parentInfo = new Dictionary<string, string>();
            var groups = MySqlConnector.select("SELECT id, iconid, displayName, parentPath, description, createdBy, creationDate, lastUpdate FROM bonita.group_;");
            List<GroupInformation> groupsList = new List<GroupInformation>();
            GroupInformation group;
            var ds = MySqlConnector.select("select displayName, id from bonita.group_;");
            parentInfo = ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row[0].ToString(), row => row[1].ToString());
            parentInfo.Add("", "");
            string existingGroups = "";
            List<string> parentIds = new List<string>();

            foreach (DataRow dr in groups.Tables[0].Rows)
            {
                group = new GroupInformation();

                group.id = dr[0].ToString();
                group.icon = dr[1].ToString();
                group.groupName = dr[2].ToString();
                group.parentName = dr[3].ToString();
                group.description = dr[4].ToString();
                group.parentId = getParentId(parentInfo, group.parentName);
                group.assignedBy = Convert.ToInt32(dr[5].ToString());
                group.creationDate = ConvertUnixTimeStamp(dr[6].ToString());
                group.updateDate = ConvertUnixTimeStamp(dr[7].ToString());

                groupsList.Add(group);
                existingGroups += "'" + group.id + "', ";
                parentIds.Add(group.parentId);
            }

            foreach (GroupInformation g in groupsList)
            {
                if (MySqlConnector.checkIfExists(g.id, "SELECT * FROM ARista.GruposBonita where IdGrupoBonita = @id;"))
                {
                    g.active = 1;
                    MySqlConnector.insertGroups(g, "update");
                }
                else
                {
                    g.active = 1;
                    MySqlConnector.insertGroups(g, "insert");
                }
            }

            existingGroups = existingGroups.Substring(0, existingGroups.Length - 2);
            MySqlConnector.update("UPDATE ARista.GruposBonita SET IdEstadoActividad = " + 2 + " WHERE IdGrupoBonita NOT IN (" + existingGroups + ")");

            string[] ids = parentIds.Distinct().ToArray();
            foreach (var id in ids)
            {
                MySqlConnector.update("UPDATE ARista.GruposBonita as t1, (select IdGrupoBonitaArista FROM ARista.GruposBonita where IdGrupoBonita =" + id + ") as t2 SET t1.IdGrupoPadreBonitaArista = CAST(t2.IdGrupoBonitaArista AS UNSIGNED) WHERE t1.IdGrupoPadreBonita = " + id + ";");
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Getting user information...");
            getUserInformation();
            Console.WriteLine("\t Done.\n");

            Console.WriteLine("Getting group information...");
            getGroupInformation();
            Console.WriteLine("\t Done.\n");

            Console.WriteLine("Getting role information...");
            getRoleInformation();
            Console.WriteLine("\t Done.\n");

            Console.WriteLine("Getting membership information...");
            getMembershipInformation();
            Console.WriteLine("\t Done.\n");
        }
    }
}
