using MySql.Data;
using MySql.Data.MySqlClient;

using BusinessEntities;
using BusinessLogic;
using System.Collections;
using System.Windows.Markup;

namespace MySqlAccess
{
    class MySqlAccess
    {

        static string connStr = "server=127.0.0.1;user=root;port=3306; password=Chrisbar1@";

        /*
        this call will represent CRUD operation
        CRUD stands for Create,Read,Update,Delete
        */
        public static void createTables()
        {
            
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "DROP DATABASE IF EXISTS Ice_Cream_Shop;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "CREATE DATABASE Ice_Cream_Shop;";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // ------- create INGREDIENTS ------- //
                 sql = "CREATE TABLE `Ice_Cream_Shop`.`INGREDIENTS` (" +
                    "`id_INGREDIENT` INT NOT NULL," +
                    "`item` VARCHAR(45) NOT NULL," +
                    "PRIMARY KEY (`id_INGREDIENT`));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // ------- create ORDERS ------- //
                sql = "CREATE TABLE `Ice_Cream_Shop`.`ORDERS` (" +
                    "`id_ORDER` INT NOT NULL, " +
                    "`id_INGREDIENT` INT NOT NULL," +
                    "`amount` INT NOT NULL, " +
                    "FOREIGN KEY (id_INGREDIENT) REFERENCES INGREDIENTS(id_INGREDIENT), " +
                    "PRIMARY KEY (id_ORDER, Id_INGREDIENT));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // ------- create SALES ------- //
                sql = "CREATE TABLE `Ice_Cream_Shop`.`SALES` (" +
                    "`id_SALE` INT NOT NULL AUTO_INCREMENT, " +
                    "`date` varchar(45) NOT NULL," +
                    "`price` INT NOT NULL," +
                    "PRIMARY KEY (`id_SALE`));";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // // create connection owner - vehicle
                // sql = "CREATE TABLE `Garage`.`Vowns` (" +
                //     "`idVown` INT NOT NULL AUTO_INCREMENT, " +
                //     "`idOwner` INT NOT NULL," +
                //     "`idVehicle` INT NOT NULL," +
                //     "PRIMARY KEY (`idVown`),"+
                //     "FOREIGN KEY (idVehicle) REFERENCES Vehicles(idVehicle)," +
                //     "FOREIGN KEY (idOwner) REFERENCES Owners(idOwner));";

                // cmd = new MySqlCommand(sql, conn);
                // cmd.ExecuteNonQuery();

                // // create connection task - vehicle
                // sql = "CREATE TABLE `Garage`.`Orders` (" +
                //     "`idOrder` INT NOT NULL AUTO_INCREMENT," +
                //     "`idVehicle` INT NOT NULL," +
                //     "`idTask` INT NOT NULL," +
                //     "`OrderDate` DATETIME DEFAULT NOW()," +
                //     "`CompleteDate` DATETIME," +
                //     "`Completed` INT NOT NULL DEFAULT 0," +
                //     "`Payed` INT NOT NULL DEFAULT 0," +
                //     "PRIMARY KEY (`idOrder`)," +
                //     "FOREIGN KEY (idVehicle) REFERENCES Vehicles(idVehicle)," +
                //     "FOREIGN KEY (idTask) REFERENCES Tasks(idTask));";

                // cmd = new MySqlCommand(sql, conn);
                // cmd.ExecuteNonQuery();   

                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void insertObjectToOrders(iceCreamOrder a)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = null;
                int id = getId();

                foreach (var item in a.fdict)
                {
                    sql = "INSERT INTO 'ice_cream_shop'. 'Orders' (`id_ORDER`,`id_INGREDIENT`,`amount')" +
                    "VALUES ('" + id + " ','" + item.Key + "," + item.Value + "');";
                    /*

                    if (obj is)
                    {
                        Owner owner = (Owner)obj;
                        sql = "INSERT INTO `Garage`.`Owners` (`Name`, `Phone`, `Address`) " +
                        "VALUES ('" + owner.getName() + "', '" + owner.getPhone() + "', '" + owner.getAddress() + "');";
                    }

                    if (obj is Vehicle)
                    {
                        Vehicle vehicle = (Vehicle)obj;
                        sql = "INSERT INTO `Garage`.`Vehicles` (`Manufacturer`, `Color`, `Year`) " +
                        "VALUES ('" + vehicle.getManufacturer() + "', '" + vehicle.getColor() + "', '" + vehicle.getYear() + "');";
                    }

                    if (obj is VTask)
                    {
                        VTask task = (VTask)obj;
                        sql = "INSERT INTO `Garage`.`Tasks` (`Name`, `Description`, `Price`) " +
                        "VALUES ('" + task.getName() + "', '" + task.getDescription() + "', '" + task.getPrice() + "');";
                    }

                    if (obj is VOwn)
                    {
                        VOwn vown = (VOwn)obj;
                        sql = "INSERT INTO `Garage`.`Vowns` (`idOwner`, `idVehicle`) " +
                        "VALUES ('" + vown.getIdOwner() + "', '" + vown.getIdVehicle() + "');";
                    }

                    if (obj is Order)
                    {
                        Order order = (Order)obj;
                        sql = "INSERT INTO `Garage`.`Orders` (`idVehicle`, `idTask`, `OrderDate`, `CompleteDate`, `Completed`, `Payed`) " +
                        "VALUES ('" + order.getIdVehicle() + "', '" + order.getIdTask()+ "', '" +
                         order.getOrderDate() + "', '" + order.getCompleteDate() + "', '" + order.getCompleted()+ "', '" + order.getPayed() + "');";
                    }
                    */

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void insertObject_Sale(Sale s)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = null;

                sql = "INSERT INTO `ice_cream_shop`.`Sales` (`date`, `price`) " +
                "VALUES ('" + s.date + "', '" + s.price + "');";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static ArrayList readAll(string tableName)
        {
            ArrayList all = new ArrayList();

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "SELECT * FROM `Garage`."+tableName;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    Object[] numb = new Object[rdr.FieldCount];
                    rdr.GetValues(numb);
                    //Array.ForEach(numb, Console.WriteLine);
                    all.Add(numb);
                }
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return all;
        }

        public  static int getId() { 
              // open connection 
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            // mysql query 

            string sql = "SELECT 'id_SALE' FROM `Ice_Cream_Shop' ORDER BY .'id_SALE' DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();
            Object[] numb = new Object[rdr.FieldCount];
            Console.WriteLine(numb[0]);
            rdr.GetValues(numb);
            int ans = (int)numb[0];
            return ans;

        }
    }

}