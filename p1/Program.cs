using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System;
using System.Threading;
using System.Text;
using System.Xml.Linq;
using Npgsql;

public class Prog
{
    static HttpListener listener;
    private static string baseUri = "http://localhost:8080/";

    private static int level = 1;
    public static bool wellcom()
    {
        Console.WriteLine("Hi you have to login in your profile ");
        Console.WriteLine("Please Inter Your email ");
        string email = Console.ReadLine();
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        using (var conn = new NpgsqlConnection(connString))
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT 1 FROM users WHERE email = @username";
                    cmd.Parameters.AddWithValue("username", email);
                    object result = cmd.ExecuteScalar();

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT level FROM users WHERE email = @username";
                    cmd.Parameters.AddWithValue("username", email);
                    object levelObject = cmd.ExecuteScalar();
                    level = Convert.ToInt32(levelObject);
                    Console.WriteLine(level);

                    conn.Close();
                    if (result != null && result != DBNull.Value) {
                        Console.WriteLine($"hi {email} please Enter your Password ");
                        string pass = Console.ReadLine();
                       
                        conn.Open();
                        using (var cmd1 = new NpgsqlCommand())
                        {
                            cmd1.Connection = conn;
                            cmd1.CommandText = "SELECT pass FROM users WHERE email = @email";
                            cmd1.Parameters.AddWithValue("email", email);
                            string storedPassword = cmd1.ExecuteScalar() as string;
                            conn.Close();
                            if (pass == storedPassword)
                            { Console.WriteLine("Password is correct!");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("Password is incorrect!");
                                return false;
                            }
                        }
                    }
                    else {
                        Console.WriteLine($"User '{email}' does not exist in the database! \n Please Try agin");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return false;
    }

    public static bool wellcomp(string email ,string pass )
    {
        
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        using (var conn = new NpgsqlConnection(connString))
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT 1 FROM users WHERE email = @username";
                    cmd.Parameters.AddWithValue("username", email);
                    object result = cmd.ExecuteScalar();

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT level FROM users WHERE email = @username";
                    cmd.Parameters.AddWithValue("username", email);
                    object levelObject = cmd.ExecuteScalar();
                    level = Convert.ToInt32(levelObject);
                    Console.WriteLine(level);

                    conn.Close();
                    if (result != null && result != DBNull.Value)
                    {
                        

                        conn.Open();
                        using (var cmd1 = new NpgsqlCommand())
                        {
                            cmd1.Connection = conn;
                            cmd1.CommandText = "SELECT pass FROM users WHERE email = @email";
                            cmd1.Parameters.AddWithValue("email", email);
                            string storedPassword = cmd1.ExecuteScalar() as string;
                            conn.Close();
                            if (pass == storedPassword)
                            {
                                Console.WriteLine("Password is correct!");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("Password is incorrect!");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"User '{email}' does not exist in the database! \n Please Try agin");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return false;
    }

    public static string showitems() {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        string final = "";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM items";
                using (var cmd = new NpgsqlCommand(sql, conn))
                { using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int itemId = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int state = reader.GetInt32(reader.GetOrdinal("state"));
                                int catid = reader.GetInt32(reader.GetOrdinal("catid"));
                                // Output the information for the item
                                final  = final+$"Item ID: {itemId}"+ $" Name: {name}"+ $" Price: {price}"+ $" Count in Inventory: {state}"+$" Category ID: {catid} \n";
                                /*Console.WriteLine($"Item ID: {itemId}");
                                Console.WriteLine($"Name: {name}");
                                Console.WriteLine($"Price: {price}");
                                Console.WriteLine($"Count in Inventory: {state}");
                                Console.WriteLine($"Category ID: {catid}");
                                Console.WriteLine();*/
                            }
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return final;
    }
    public static string showcat()
    {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        string final = "";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM category";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int catId = reader.GetInt32(reader.GetOrdinal("cateid"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string description = reader.GetString(reader.GetOrdinal("description"));
                                // Output the information for the item
                                final = final + $"Category ID: {catId}"+ $" Name: {name}"+ $" description: {description} \n";
                              /*  Console.WriteLine($"Category ID: {catId}");
                                Console.WriteLine($"Name: {name}");
                                Console.WriteLine($"description: {description}");
                                Console.WriteLine();*/
                            }
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return final;
    }
    public static void additem() {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Specify the details of the new item
                Console.WriteLine("Enter id");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter name");
                string itemName = Console.ReadLine(); ;
                Console.WriteLine("Enter price");
                int itemPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter how much in inventory");
                int state = int.Parse(Console.ReadLine());
                Console.WriteLine(" this is the categories");
                showcat();
                Console.WriteLine("Now enter the category id ");
                int catid = int.Parse(Console.ReadLine());

                // Construct the SQL query to insert the new item
                string sql = "INSERT INTO public.items(id,name,price,state,catid) VALUES (@id,@name,@price,@state,@catid)";

                // Create a NpgsqlCommand object with the SQL query and connection
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (item details)
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", itemName);
                    cmd.Parameters.AddWithValue("price", itemPrice);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("catid", catid);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("New item added to the inventory successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add the new item to the inventory.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void addcat()
    {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Specify the details of the new item
                Console.WriteLine("Enter id");
                int cateid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter name");
                string name = Console.ReadLine(); ;
                Console.WriteLine("Enter description");
                string description = Console.ReadLine();
                

                // Construct the SQL query to insert the new item
                string sql = "INSERT INTO public.category(cateid,name,description) VALUES (@cateid,@name,@description)";

                // Create a NpgsqlCommand object with the SQL query and connection
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (item details)
                    cmd.Parameters.AddWithValue("cateid", cateid);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("description", description);
                   

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("New category added to the inventory successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add the new category to the inventory.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void updatecat() {
        Console.WriteLine("Enter id for the category that you want to update the information for it");
        int cateid = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter name");
        string name = Console.ReadLine(); ;
        Console.WriteLine("Enter description");
        string description = Console.ReadLine();
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Construct the SQL query to update the item
                string sql = "UPDATE public.category SET name= @name, description= @description WHERE cateid= @cateid";

                // Create a NpgsqlCommand object with the SQL query and connection
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (new item details and item ID)
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("cateid", cateid);


                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Category with ID {cateid} updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Category with ID {cateid} not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void updateinformation()
    {
        Console.WriteLine("Enter id for the item that you want to update the information for it");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter name");
        string itemName = Console.ReadLine(); ;
        Console.WriteLine("Enter price");
        int itemPrice = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter how much in inventory");
        int state = int.Parse(Console.ReadLine());
        Console.WriteLine(" this is the categories");
        showcat();
        Console.WriteLine("Now enter the category id ");
        int catid = int.Parse(Console.ReadLine());
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Construct the SQL query to update the item
                string sql = "UPDATE public.items SET name=@name, price=@price, state=@state, catid=@catid WHERE id = @id";

                // Create a NpgsqlCommand object with the SQL query and connection
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (new item details and item ID)
                    cmd.Parameters.AddWithValue("name", itemName);
                    cmd.Parameters.AddWithValue("price", itemPrice);
                    cmd.Parameters.AddWithValue("state", state);
                    cmd.Parameters.AddWithValue("catid", catid);
                    cmd.Parameters.AddWithValue("id", id);


                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Item with ID {id} updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Item with ID {id} not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void deletitems(int id) {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "DELETE FROM items WHERE id =@id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (in this case, the item ID)
                    cmd.Parameters.AddWithValue("id", id);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Item with ID {id} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Item with ID {id} not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void deletcat(int id)
    {
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "DELETE FROM category WHERE cateid =@cateid";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    // Add parameters to the query (in this case, the item ID)
                    cmd.Parameters.AddWithValue("cateid", id);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Category with ID {id} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Category with ID {id} not found.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void searchitembyid()
    {
        Console.WriteLine("Enter The id of item you want to search:");
        int id =int.Parse(Console.ReadLine());
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
       
        
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM public.items WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id",id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int state = reader.GetInt32(reader.GetOrdinal("state"));
                                int catid = reader.GetInt32(reader.GetOrdinal("catid"));
                                // Output the information for the item
                                Console.WriteLine($"Item ID: {id}");
                                Console.WriteLine($"Name: {name}");
                                Console.WriteLine($"Price: {price}");
                                Console.WriteLine($"Count in Inventory: {state}");
                                Console.WriteLine($"Category ID: {catid}");
                                Console.WriteLine();
                            
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void Sstatfilter()
    {
        Console.WriteLine("Enter the number of what you want to see \n 1-in stock \n 2- out of stock  \n 3-low stock");
        int stock = int.Parse(Console.ReadLine());
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM items";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int itemId = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int state = reader.GetInt32(reader.GetOrdinal("state"));
                                int catid = reader.GetInt32(reader.GetOrdinal("catid"));
                                if (state>50 && stock==1) {
                                Console.WriteLine($"Item ID: {itemId}");
                                Console.WriteLine($"Name: {name}");
                                Console.WriteLine($"Price: {price}");
                                Console.WriteLine($"Count in Inventory: {state}");
                                Console.WriteLine($"Category ID: {catid}");
                                Console.WriteLine(); }
                                else if (state < 50 && state > 0 && stock == 3)
                                {
                                    Console.WriteLine($"Item ID: {itemId}");
                                    Console.WriteLine($"Name: {name}");
                                    Console.WriteLine($"Price: {price}");
                                    Console.WriteLine($"Count in Inventory: {state}");
                                    Console.WriteLine($"Category ID: {catid}");
                                    Console.WriteLine();
                                }
                                else if (state == 0 && stock == 2)
                                {
                                    Console.WriteLine($"Item ID: {itemId}");
                                    Console.WriteLine($"Name: {name}");
                                    Console.WriteLine($"Price: {price}");
                                    Console.WriteLine($"Count in Inventory: {state}");
                                    Console.WriteLine($"Category ID: {catid}");
                                    Console.WriteLine();
                                }

                                // Output the information for the item


                            }
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void catfilter()
    {
        Console.WriteLine("Enter the category id that you want from this list");
        showcat();
        int cat = int.Parse(Console.ReadLine());
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        try
        {    
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM items WHERE catid = @catid";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("catid", cat);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int itemId = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int state = reader.GetInt32(reader.GetOrdinal("state"));
                                int catid = reader.GetInt32(reader.GetOrdinal("catid"));
                              
                                    Console.WriteLine($"Item ID: {itemId}");
                                    Console.WriteLine($"Name: {name}");
                                    Console.WriteLine($"Price: {price}");
                                    Console.WriteLine($"Count in Inventory: {state}");
                                    Console.WriteLine($"Category ID: {catid}");
                                    Console.WriteLine();
                                

                            }
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void quantfilter()
    {
        Console.WriteLine("Enter the quantity that you want to filter the items on it");
        int q = int.Parse(Console.ReadLine());
        string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";
        try
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT * FROM items WHERE state = @state";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("state", q);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int itemId = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int state = reader.GetInt32(reader.GetOrdinal("state"));
                                int catid = reader.GetInt32(reader.GetOrdinal("catid"));

                                Console.WriteLine($"Item ID: {itemId}");
                                Console.WriteLine($"Name: {name}");
                                Console.WriteLine($"Price: {price}");
                                Console.WriteLine($"Count in Inventory: {state}");
                                Console.WriteLine($"Category ID: {catid}");
                                Console.WriteLine();


                            }
                        }
                        else
                        {
                            Console.WriteLine("No items found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void HandleRequest(HttpListenerContext context)
    {
        // Get the HTTP method and request URL
        string method = context.Request.HttpMethod;
        string url = context.Request.Url.AbsolutePath;

        // Check for Basic Authorization header
        if (context.Request.Headers["Authorization"] == null || !context.Request.Headers["Authorization"].StartsWith("Basic "))
        {
            // If Authorization header is missing or not Basic, send 401 Unauthorized response
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.AddHeader("WWW-Authenticate", "Basic realm=\"My Realm\"");
            context.Response.Close();
            return;
        }

        // Decode the Authorization header
        string encodedCredentials = context.Request.Headers["Authorization"].Substring("Basic ".Length).Trim();
        string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        string[] parts = credentials.Split(':');
        string username = parts[0];
        string password = parts[1];
        
        // Check username and password (for demonstration purposes)
        if (wellcomp(username, password) ==true)
        {
            // Handle different HTTP methods and URLs
            
            if (method == "GET" && url == "/showitems")
            {
                // Handle GET request for retrieving items
                // Send back a JSON response with the list of items
                string responseJson = showitems();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (method == "GET" && url == "/showcat" && level == 1)
            {
                // Handle GET request for retrieving items
                // Send back a JSON response with the list of items
                string responseJson = showcat();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (method == "PUT" && url.StartsWith("/newitems/"))
            {
                // Extract the ID from the URL
                string[] urlParts = url.Split('/');
                string responseJson;
                string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();

                        // Specify the details of the new item
                        // Console.WriteLine("Enter id");
                        int id = int.Parse(urlParts[2]);
                        //Console.WriteLine("Enter name");
                        string itemName = urlParts[3]; ;
                        // Console.WriteLine("Enter price");
                        int itemPrice = int.Parse(urlParts[4]);
                        // Console.WriteLine("Enter how much in inventory");
                        int state = int.Parse(urlParts[5]);
                        // Console.WriteLine(" this is the categories");
                        // showcat();
                        // Console.WriteLine("Now enter the category id ");
                        int catid = int.Parse(urlParts[6]);

                        // Construct the SQL query to insert the new item
                        string sql = "INSERT INTO public.items(id,name,price,state,catid) VALUES (@id,@name,@price,@state,@catid)";

                        // Create a NpgsqlCommand object with the SQL query and connection
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            // Add parameters to the query (item details)
                            cmd.Parameters.AddWithValue("id", id);
                            cmd.Parameters.AddWithValue("name", itemName);
                            cmd.Parameters.AddWithValue("price", itemPrice);
                            cmd.Parameters.AddWithValue("state", state);
                            cmd.Parameters.AddWithValue("catid", catid);

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                responseJson = "New item added to the inventory successfully.";
                            }
                            else
                            {
                                responseJson = "Failed to add the new item to the inventory.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseJson = $"Error: {ex.Message}";
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            }
            else if (method == "PUT" && url.StartsWith("/newcat/") && level == 1)
            {// Extract the ID from the URL
                string[] urlParts = url.Split('/');
                string responseJson = "";
                string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();

                        // Specify the details of the new item
                        //  Console.WriteLine("Enter id");
                        int cateid = int.Parse(urlParts[2]);
                        // Console.WriteLine("Enter name");
                        string name = urlParts[3];
                        //Console.WriteLine("Enter description");
                        string description = urlParts[4];


                        // Construct the SQL query to insert the new item
                        string sql = "INSERT INTO public.category(cateid,name,description) VALUES (@cateid,@name,@description)";

                        // Create a NpgsqlCommand object with the SQL query and connection
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            // Add parameters to the query (item details)
                            cmd.Parameters.AddWithValue("cateid", cateid);
                            cmd.Parameters.AddWithValue("name", name);
                            cmd.Parameters.AddWithValue("description", description);


                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                responseJson = "New category added to the inventory successfully.";
                            }
                            else
                            {
                                responseJson = "Failed to add the new category to the inventory.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseJson = $"Error: {ex.Message}";
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (method == "PUT" && url.StartsWith("/updatecat/") && level == 1)
            {
                string[] urlParts = url.Split('/');
                string responseJson;
                //Console.WriteLine("Enter id for the category that you want to update the information for it");
                int cateid = int.Parse(urlParts[2]);
               // Console.WriteLine("Enter name");
                string name = urlParts[3]; ;
                //Console.WriteLine("Enter description");
                string description = urlParts[4];
                string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        // Construct the SQL query to update the item
                        string sql = "UPDATE public.category SET name= @name, description= @description WHERE cateid= @cateid";

                        // Create a NpgsqlCommand object with the SQL query and connection
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            // Add parameters to the query (new item details and item ID)
                            cmd.Parameters.AddWithValue("name", name);
                            cmd.Parameters.AddWithValue("description", description);
                            cmd.Parameters.AddWithValue("cateid", cateid);


                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                responseJson=$"Category with ID {cateid} updated successfully.";
                            }
                            else
                            {
                                responseJson=$"Category with ID {cateid} not found.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseJson=$"Error: {ex.Message}";
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (method == "PUT" && url.StartsWith("/updateitems/")) {
                string[] urlParts = url.Split('/');
                string responseJson="";
              
                int id = int.Parse(urlParts[2]);            
                string itemName = urlParts[3];    
                int itemPrice = int.Parse(urlParts[4]);
                int state = int.Parse(urlParts[5]);
                int catid = int.Parse(urlParts[6]);
                string connString = "Host=localhost;Username=postgres;Password=;Database=Invent";

                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        // Construct the SQL query to update the item
                        string sql = "UPDATE public.items SET name=@name, price=@price, state=@state, catid=@catid WHERE id = @id";

                        // Create a NpgsqlCommand object with the SQL query and connection
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            // Add parameters to the query (new item details and item ID)
                            cmd.Parameters.AddWithValue("name", itemName);
                            cmd.Parameters.AddWithValue("price", itemPrice);
                            cmd.Parameters.AddWithValue("state", state);
                            cmd.Parameters.AddWithValue("catid", catid);
                            cmd.Parameters.AddWithValue("id", id);


                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                responseJson = $"Item with ID {id} updated successfully.";
                            }
                            else
                            {
                                responseJson = $"Item with ID {id} not found.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseJson = $"Error: {ex.Message}";
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson);         
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            
            
            else
            {
                // Handle other HTTP methods or URLs
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
        else
        {
            // If username or password is incorrect, send 401 Unauthorized response
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.Close();
            return;
        }

        // Close the response stream
        context.Response.Close();
    }
  
    static async Task Main(string[] args)
    {
        string serv;
        Console.WriteLine("Lets start our Application if you want to use post man enter 1 and 2 or any number to use console app");
        int p = int.Parse(Console.ReadLine());
        if (p == 1) { 
        // Create a new HttpListener instance
        listener = new HttpListener();
        listener.Prefixes.Add(baseUri);
        // Start listening for incoming requests
        listener.Start();
        Console.WriteLine("Listening for incoming requests...");

        // Handle requests asynchronously
        ThreadPool.QueueUserWorkItem((_) =>
        {
            while (true)
            {
                // Wait for an incoming request
                HttpListenerContext context = listener.GetContext();

                // Process the request in a separate thread
                ThreadPool.QueueUserWorkItem((ctx) =>
                {
                    HandleRequest((HttpListenerContext)ctx);
                }, context);
            }
        });

        // Keep the console application running
        Console.WriteLine("Press any key to stop...");
        Console.ReadLine();

        // Stop listening for requests
        listener.Stop();
    }
        else {
         Console.WriteLine("Lets start our Application");
        bool isLoggedIn =  wellcom();
        if (isLoggedIn)
        {

            if (level == 1)
            {
                int exite = 1;
                while (exite == 1)
                {
                    Console.WriteLine("Enter the number of the service that you want \n 1-show alll items \n" +
                        " 2-add new item in Inventory \n 3-delete item in Inventory \n 4-update item in Inventory \n 5-show all Categories" +
                        "\n 6-add new Categories \n 7-delete categories \n 8-update categories \n 9-make search on specific item \n 10-filtering on  state" +
                        "\n 11-filtering on categorie \n 12-filtering on quantity  \n 13-exit");

                    serv = Console.ReadLine();
                    switch (serv)
                    {
                        case "1":
                            Console.WriteLine( showitems());
                            break;
                        case "2":
                            additem();
                            break;
                        case "3":
                            Console.WriteLine("Enter id for the item ");
                            int id = int.Parse(Console.ReadLine());
                            deletitems(id);
                            break;
                        case "4":
                            updateinformation();
                            break;

                        case "5":
                            showcat();
                            break;
                        case "6":
                            addcat();
                            break;
                        case "7":
                            Console.WriteLine("Enter id for the item ");
                            int id2 = int.Parse(Console.ReadLine());
                            deletcat(id2);
                            break;
                        case "8":
                            updatecat();
                            break;
                        case "9":
                            searchitembyid();
                            break;
                        case "10":
                            Sstatfilter();
                            break;
                        case "11":
                            catfilter();
                            break;
                        case "12":
                            quantfilter();
                            break;
                        case "13":
                            Console.WriteLine("Logout from program");
                            exite = 0;
                            break;

                        default:
                            Console.WriteLine("Wrong value Please enter again ");
                            break;

                    }
                }
            }
            else
            {
                int exite = 1;
                while (exite == 1)
                {
                    Console.WriteLine("Enter the number of the service that you want \n 1-show alll items \n" +
                        " 2-add new item in Inventory \n 3-delete item in Inventory \n 4-update item in Inventory \n 5-make search on specific item " +
                        "\n 6-filtering on  state \n 7-filtering on categorie \n 8-filtering on quantity   \n 9-exit");

                    serv = Console.ReadLine();
                    switch (serv)
                    {
                        case "1":
                                Console.WriteLine(showitems());
                                break;
                        case "2":
                            additem();
                            break;
                        case "3":
                            Console.WriteLine("Enter id for the item ");
                            int id = int.Parse(Console.ReadLine());
                            deletitems(id);
                            break;
                        case "4":
                            updateinformation();
                            break;
                        case "5":
                            searchitembyid();
                            break;
                        case "6":
                            Sstatfilter();
                            break;
                        case "7":
                            catfilter();
                            break;
                        case "8":
                            quantfilter();
                            break;

                        case "9":
                            Console.WriteLine("Logout from program");
                            exite = 0;
                            break;
                        default:
                            Console.WriteLine("Wrong value Please enter again ");
                            break;

                    }
                }
            }
        }
  
   }
    
    }

   
}