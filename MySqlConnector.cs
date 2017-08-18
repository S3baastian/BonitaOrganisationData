using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitaOrganisationData
{
    class MySqlConnector
    {

        const string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
        const string DB_CONN_STR_BONITA = "Server=10.0.0.37;Port=3306;Uid=bonita;Pwd=bpm;Database=test;";

        public static DataSet select(string queryStr)
        {

            var ds = new DataSet();

            //constk string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
            //const string DB_CONN_STR = "Server=10.0.0.37;Port=3306;Uid=bonita;Pwd=bpm;Database=test;";
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR_BONITA);

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(queryStr, connection);
                var adapter = new MySqlDataAdapter(command);

                adapter.Fill(ds);
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }

        public static DataSet selectFromArista(string queryStr)
        {

            var ds = new DataSet();

            //constk string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
           // const string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(queryStr, connection);
                var adapter = new MySqlDataAdapter(command);

                adapter.Fill(ds);
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }

        public static bool insert(string queryStr)
        {
            const string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);


            connection.Open();

            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand command = new MySqlCommand(queryStr, connection, trans);

            if (command.ExecuteNonQuery() == 1)
            {
                trans.Commit();
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }


        }

        public static bool update(string queryStr)
        {
            //const string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;"; 
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);


            connection.Open();

            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand command = new MySqlCommand(queryStr, connection, trans);

             
            try
            {
                command.ExecuteNonQuery();
                trans.Commit();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                return false;
            }


        }

        public static bool checkIfExists(string id, string query)
        {
            //const string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
            int? userCount = null;

            using (MySqlConnection mySqlConnection = new MySqlConnection(DB_CONN_STR))
            {
                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    try
                    {
                        mySqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("@id", id);

                        try { userCount = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString()); }
                        catch { userCount = (int?)sqlCommand.ExecuteScalar(); }


                        mySqlConnection.Close();
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                    if (userCount != null) return true;
                    else return false;
                }
            }


        }

        public static void insertUsers(UserInformation user, string queryType)
        {

            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {


                queryStr = "INSERT INTO ARista.UsuariosBonita (IdUsuarioBonita,NombreUsuario,Contraseña,ConfirmarContraseña,Avatar,Nombre,Apellido,Titulo,Profesion,IdResponsableBonitaArista,DireccionEmpresa,PaisEmpresa,CodigoPostalEmpresa,EstadoEmpresa,CorreoElectronicoEmpresa,TelefonoEmpresa,MovilEmpresa,DireccionPersonal,PaisPersonal,CodigoPostalPersonal,CorreoElectronicoPersonal,EstadoPersonal,TelefonoPersonal,MovilPersonal,IdEstadoActividad,IdUsuarioCreador, FechaCreacion, FechaUltimaModificacion)"
                                       + "VALUES (@id, @userName, @password, @password, @icon, @firstName, @lastName, @title, @jobTitle, @managerId, @address, @country, @zipCode, @state, @email, @phoneNumber, @mobileNumber, @addressPersonal, @countryPersonal, @zipCodePersonal, @emailPersonal, @statePersonal, @phoneNumberPersonal, @mobileNumberPersonal, @active, @assignedBy, @creationDate, @updateDate);";

            }
            else
            {
                queryStr = "UPDATE ARista.UsuariosBonita SET NombreUsuario = @userName, Contraseña = @password, ConfirmarContraseña = @password, Avatar = @icon, Nombre = @firstName, Apellido = @lastName, Titulo = @title, Profesion = @jobTitle, IdResponsableBonitaArista = @managerId, DireccionEmpresa = @address, PaisEmpresa = @country,"
                       + "CodigoPostalEmpresa = @zipCode, EstadoEmpresa = @state, CorreoElectronicoEmpresa = @email, TelefonoEmpresa = @phoneNumber, MovilEmpresa = @mobileNumber, DireccionPersonal = @addressPersonal,PaisPersonal = @countryPersonal, CodigoPostalPersonal = @zipCodePersonal, CorreoElectronicoPersonal = @emailPersonal,"
                       + "EstadoPersonal = @statePersonal, TelefonoPersonal = @phoneNumberPersonal ,MovilPersonal = @mobileNumberPersonal, IdEstadoActividad = @active, IdUsuarioCreador = @assignedBy, FechaCreacion = @creationDate, FechaUltimaModificacion = @updateDate WHERE IdUsuarioBonita = @id;";
            }

            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);


            cmd.Parameters.AddWithValue("@id", user.id);
            cmd.Parameters.AddWithValue("@userName", user.userName);
            cmd.Parameters.AddWithValue("@password", user.password);
            cmd.Parameters.AddWithValue("@icon", user.icon);
            cmd.Parameters.AddWithValue("@firstName", user.firstName);
            cmd.Parameters.AddWithValue("@lastName", user.lastName);
            cmd.Parameters.AddWithValue("@title", user.title);
            cmd.Parameters.AddWithValue("@jobTitle", user.jobTitle);
            cmd.Parameters.AddWithValue("@managerId", user.managerId);
            cmd.Parameters.AddWithValue("@address", user.address);
            cmd.Parameters.AddWithValue("@country", user.country);
            cmd.Parameters.AddWithValue("@zipCode", user.zipCode);
            cmd.Parameters.AddWithValue("@state", user.state);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
            cmd.Parameters.AddWithValue("@mobileNumber", user.mobileNumber);
            cmd.Parameters.AddWithValue("@addressPersonal", user.addressPersonal);
            cmd.Parameters.AddWithValue("@countryPersonal", user.countryPersonal);
            cmd.Parameters.AddWithValue("@zipCodePersonal", user.zipCodePersonal);
            cmd.Parameters.AddWithValue("@emailPersonal", user.emailPersonal);
            cmd.Parameters.AddWithValue("@statePersonal", user.statePersonal);
            cmd.Parameters.AddWithValue("@phoneNumberPersonal", user.phoneNumberPersonal);
            cmd.Parameters.AddWithValue("@mobileNumberPersonal", user.mobileNumberPersonal);
            cmd.Parameters.AddWithValue("@active", user.enabled);
            cmd.Parameters.AddWithValue("@assignedBy", user.assignedBy);
            cmd.Parameters.AddWithValue("@creationDate", user.creationDate);
            cmd.Parameters.AddWithValue("@updateDate", user.updateDate);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }



            trans.Commit();
            connection.Close();

        }

        public static void updateUsers(string queryStr)
        {
            //string queryStr = "UPDATE ARista.UsuariosBonita SET NombreUsuario = user.userName,Contraseña = user.password,ConfirmarContraseña = user.password,Avatar = user.icon,Nombre = user.firstName,Apellido user.lastName,Titulo = user.title,Profesion = user.jobTitle,IdResponsableBonitaArista = user.managerId,DireccionEmpresa = user.address,PaisEmpresa = user.country,"
            //                + "CodigoPostalEmpresa = user.zipCode,EstadoEmpresa = user.state,CorreoElectronicoEmpresa = user.email,TelefonoEmpresa = user.phoneNumber,MovilEmpresa = user.mobileNumber,DireccionPersonal = user.addressPersonal,PaisPersonal = user.countryPersonal,CodigoPostalPersonal = user.zipCodePersonal,CorreoElectronicoPersonal = user.emailPersonal,"
            //                + "EstadoPersonal = user.statePersonal,TelefonoPersonal = user.phoneNumberPersonal,MovilPersonal = user.mobileNumberPersonal WHERE IdUsuarioBonita = user.id;";

            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            connection.Open();

            MySqlTransaction trans = connection.BeginTransaction();


            MySqlCommand command = new MySqlCommand(queryStr, connection, trans);


            try { command.ExecuteNonQuery(); }
            catch { }


            trans.Commit();
            connection.Close();

        }

        public static void insertGroups(GroupInformation group, string queryType)
        {
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {

                queryStr = "INSERT INTO ARista.GruposBonita (IdGrupoBonita, Avatar, Nombre, IdGrupoPadreBonita, Descripcion, RutaArbolGruposBonita, IdEstadoActividad, IdUsuarioCreador, FechaCreacion, FechaUltimaModificacion) "
                                   + "VALUES (@id, @iconid, @name, @parentId, @description, @parentPath, @active, @assignedBy, @creationDate, @updateDate);";

            }
            else
            {
                queryStr = "UPDATE ARista.GruposBonita SET  Avatar = @iconid, Nombre = @name, IdGrupoPadreBonita = @parentId, Descripcion = @description, RutaArbolGruposBonita = @parentPath, IdEstadoActividad = @active, IdUsuarioCreador = @assignedBy, FechaCreacion = @creationDate, FechaUltimaModificacion = @updateDate"
                                      + " WHERE IdGrupoBonita = @id";
            }


            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);

            //IdGrupoBonita, Avatar, Nombre, IdGrupoPadreBonitaArista, Descripcion
            cmd.Parameters.AddWithValue("@id", group.id);
            cmd.Parameters.AddWithValue("@iconid", group.icon);
            cmd.Parameters.AddWithValue("@name", group.groupName);
            cmd.Parameters.AddWithValue("@parentId", group.parentId);
            cmd.Parameters.AddWithValue("@description", group.description);
            cmd.Parameters.AddWithValue("@parentPath", group.parentName);
            cmd.Parameters.AddWithValue("@active", group.active);
            cmd.Parameters.AddWithValue("@assignedBy", group.assignedBy);
            cmd.Parameters.AddWithValue("@creationDate", group.creationDate);
            cmd.Parameters.AddWithValue("@updateDate", group.updateDate);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }



            trans.Commit();
            connection.Close();

        }

        public static void insertRoles(RoleInformation role, string queryType)
        {
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {

                queryStr = "INSERT INTO ARista.RolesBonita (IdRolBonita, Avatar, Nombre, TituloDinamico, Descripcion, IdEstadoActividad, IdUsuarioCreador, FechaCreacion) "
                                   + "VALUES (@id, @iconid, @name, @dinamicName, @description, @active, @assignedBy, @creationDate);";

            }
            else
            {
                queryStr = "UPDATE ARista.RolesBonita SET  Avatar = @iconid, Nombre = @name, TituloDinamico = @dinamicName, Descripcion = @description, IdEstadoActividad = @active, IdUsuarioCreador = @assignedBy, FechaCreacion = @creationDate"
                                      + " WHERE IdRolBonita = @id";
            }


            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);

            //IdGrupoBonita, Avatar, Nombre, IdGrupoPadreBonitaArista, Descripcion
            cmd.Parameters.AddWithValue("@id", role.id);
            cmd.Parameters.AddWithValue("@iconid", role.icon);
            cmd.Parameters.AddWithValue("@name", role.name);
            cmd.Parameters.AddWithValue("@dinamicName", role.dinamicName);
            cmd.Parameters.AddWithValue("@description", role.description);
            cmd.Parameters.AddWithValue("@active", role.active);
            cmd.Parameters.AddWithValue("@assignedBy", role.assignedBy);
            cmd.Parameters.AddWithValue("@creationDate", role.creationDate);



            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }



            trans.Commit();
            connection.Close();

        }


        public static void insertMembership(MembershipInformation member, string queryType)
        {
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {

                queryStr = "INSERT INTO ARista.MembresiasBonitaRel (IdMembresiaBonita, IdGrupoBonitaArista, IdRolBonitaArista, IdUsuarioBonitaArista, IdEstadoActividad, IdUsuarioAsignador, FechaAsignacion) "
                                   + "VALUES (@id, @groupId, @roleid, @userId, @active, @assignedBy, @assignedDate);";

            }
            else
            {
                queryStr = "UPDATE ARista.MembresiasBonitaRel SET IdGrupoBonitaArista = @groupId, IdRolBonitaArista = @roleid, IdUsuarioBonitaArista = @userId, IdEstadoActividad = @active, IdUsuarioAsignador = @assignedBy, FechaAsignacion = @assignedDate "
                                      + " WHERE IdMembresiaBonita = @id";
            }


            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);

            //IdGrupoBonita, Avatar, Nombre, IdGrupoPadreBonitaArista, Descripcion
            cmd.Parameters.AddWithValue("@id", member.id);
            cmd.Parameters.AddWithValue("@roleid", member.roleId);
            cmd.Parameters.AddWithValue("@userId", member.userId);
            cmd.Parameters.AddWithValue("@groupId", member.groupId);
            cmd.Parameters.AddWithValue("@active", member.active);
            cmd.Parameters.AddWithValue("@assignedBy", member.assignedBy);
            cmd.Parameters.AddWithValue("@assignedDate", member.assignedDate);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }



            trans.Commit();
            connection.Close();

        }
    }
}
