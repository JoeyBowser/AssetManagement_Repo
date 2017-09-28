//© [2017] Joey Bowser
//THIS SCRIPT RUNS A PROGRAM FOR A BASIC ASSET CREATION
//THE USER WILL BE ABLE TO CREATE OBJECTS AND EDIT THEIR VALUES LIKE NAME, POSTIONS, SCALE, ETC.
//THE DATA CAN ALSO BE WRITTEN TO A TEXT FILE INCASE THE USER WANTS TO IMPORT THE INFO ELSEWHERE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;

namespace AssetManagement
{
    //MAIN PROGRAM/CLASS
    class Program
    {
        static void Main(string[] args)
        {
            bool stopApp = false;
            string input;
            //FOR CALLING THE FUNCTIONS IN THE OTHER CLASS
            Objects myObjects;
            myObjects = new Objects();
            //WHILE BOOL IS FALSE, RUN WHILE LOOP AND MENU OPTIONS
            while (stopApp == false)
            {
                Console.WriteLine("------------------------------------------------------------------" +
                    "\nPlease select an action: \n-------------------------------------" +
                    "----------------------------- \nA = Create Object \nB = Edit Object Name" +
                    "\nC = Control Object \nD = Display All Objects \nE = Delete Object \nF = Export Object \nQ = Quit Application");
                input = Console.ReadLine();

                switch (input)
                {
                    case "A":
                        myObjects.CreateObj();
                        break;
                    case "B":
                        myObjects.EditObj();
                        break;
                    case "C":
                        myObjects.CtrlObj();
                        break;
                    case "D":
                        myObjects.CountObj();
                        break;
                    case "E":
                        myObjects.DeleteObj();
                        break;
                    case "F":
                        myObjects.ExportObj();
                        break;
                    case "Q":
                        Console.WriteLine("Program now exiting... \n(Press any key to continue)");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid Key.");
                        break;
                }
            }
        }
    }
    //THIS CLASS IS USED TO MANAGE THE OBJECTS AND THEIR ATTRIBUTES
    class Objects
    {
        //INPUTS FOR THE DIFFERENT MENU OPTIONS
        public string inputA;
        public string inputB;
        public string inputC;
        public string inputD;
        //public string inputE;
        public string inputF;
        public string inputNum;
        //VARIABLES FOR THE CURRENTLY SELECTED OBJECTS
        public int currNum = 0;
        public string currPos;
        public string currRot;
        public string currScale;
        //VARIABLES FOR THE DIFFERENT ATTRIBUTES
        public string objNum;
        public string objName;
        public string objType;
        public string posX = "0";
        public string posY = "0";
        public string posZ = "0";
        public string rotX = "0";
        public string rotY = "0";
        public string rotZ = "0";
        public string scaleX = "0";
        public string scaleY = "0";
        public string scaleZ = "0";

        public string newName;
        //BOOLEANS FOR DIFFERENT OPTIONS
        public bool createDone = false;
        public bool editDone = false;
        public bool ctrlDone = false;
        public bool deleteDone = false;
        public bool exportDone = false;
        //ARRAY FOR SEPARATING OBJECT STRINGS INTO MULTIPLE STRINGS
        string[] objArray = new string[6];
        //LIST FOR STORING OBJECTS
        List<string> objStats = new List<string>();
        //FOR CREATING OBJECTS
        public void CreateObj()
        {
            posX = "0";
            posY = "0";
            posZ = "0";
            rotX = "0";
            rotY = "0";
            rotZ = "0";
            scaleX = "0";
            scaleY = "0";
            scaleZ = "0";

            createDone = false;

            while (createDone == false)
            {
                Console.WriteLine("\nPlease select an object shape to create: \n-------------------------------------" +
                    "\nA = Sphere \nB = Cube \nC = Cylinder \nM = Previous Menu \nQ = Quit Application");
                inputA = Console.ReadLine();

                switch (inputA)
                {
                    case "A":
                        objName = "Object Name: Sphere";
                        objType = "Object Type: Sphere";
                        ObjInfo();
                        break;
                    case "B":
                        objName = "Object Name: Cube";
                        objType = "Object Type: Cube";
                        ObjInfo();
                        break;
                    case "C":
                        objName = "Object Name: Cylinder";
                        objType = "Object Type: Cylinder";
                        ObjInfo();
                        break;
                    case "M":
                        createDone = true;
                        break;
                    case "Q":
                        Console.WriteLine("Program now exiting... \n(Press any key to continue)");
                        Console.ReadLine();
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid Key.");
                        //Console.ReadLine();
                        break;
                }
            }
        }
        //OBJECT INFO THAT IS REUSED MULTIPLE TIMES CAN BE CALLED FROM HERE
        public void ObjInfo()
        {
            var tempNum = currNum + 1;
            objNum = "Object Number: " + tempNum.ToString();

            currPos = "Postion: " + posX + "," + posY + "," + posZ;
            currRot = "Rotation: " + rotX + "," + rotY + "," + rotZ;
            currScale = "Scale: " + scaleX + "," + scaleY + "," + scaleZ;
            objArray[0] = objNum.ToString();
            objArray[1] = objName;
            objArray[2] = objType;
            objArray[3] = currPos;
            objArray[4] = currRot;
            objArray[5] = currScale;

            string objArrayString = JoinArray(objArray);
            objStats.Add(objArrayString);

            Console.WriteLine("\n" + objStats[currNum]);
            Console.WriteLine("\nCurrent object count: " + objStats.Count);
            currNum = currNum + 1;
        }
        //FOR EDITING THINGS LIKE OBJECT NAME
        public void EditObj()
        {
            editDone = false;

            while (editDone == false)
            {
                //CHECK TO SEE IF THERE IS ATLEAST 1 OBJECT THAT'S ALREADY BEEN CREATED
                if (objStats.Count < 1)
                {
                    Console.WriteLine("Please create an object first!");
                    CreateObj();
                    editDone = true;
                }
                else
                {
                    Console.WriteLine("\nPlease select an option: \n-------------------------------------" +
                        "\nA = Edit Object Name \nM = Previous Menu \nQ = Quit Application");
                    inputB = Console.ReadLine();

                    switch (inputB)
                    {
                        case "A":
                            EditName();
                            break;
                        case "M":
                            editDone = true;
                            break;
                        case "Q":
                            Console.WriteLine("Program now exiting... \n(Press any key to continue)");
                            Console.ReadLine();
                            System.Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("Invalid Key.");
                            break;
                    }
                }
            }
        }
        //FOR ACTUALLY EDITING OBJECT NAME
        public void EditName()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with the object that has the name you would like to change:");
                inputNum = Console.ReadLine();
                //MAKE SURE USER INPUT MATCHES CERTAIN CHARACTERS
                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            var statsArray = objStats[tempNum].Split('\n');

            inputEntered2 = false;

            while (inputEntered2 == false)
            {
                Console.WriteLine("Current name: " + statsArray[1] + "\nPlease enter in new name for object: ");
                newName = Console.ReadLine();

                if (newName != null)
                {
                    inputEntered2 = true;
                }
                else
                {
                    Console.WriteLine("Please enter in a valid name for object.");
                }
            }
            statsArray[1] = "Object Name: " + newName;

            Console.WriteLine("------------------");

            foreach (object o in statsArray)
            {
                Console.WriteLine(o);
            }

            string objArrayString = JoinArray(statsArray);
            objStats[tempNum] = objArrayString;
        }
        //FOR CONTROLLING OBJECT'S MOVEMENT AND ALL
        public void CtrlObj()
        {
            ctrlDone = false;

            while (ctrlDone == false)
            {
                if (objStats.Count < 1)
                {
                    Console.WriteLine("Please create an object first!");
                    CreateObj();
                    ctrlDone = true;
                }
                else
                {
                    Console.WriteLine("\nPlease select an option: \n-------------------------------------" +
                        "\nA = Edit Object Position \nB = Edit Object Rotation \nC = Edit Object Scale \nM = Previous Menu \nQ = Quit Application");
                    inputC = Console.ReadLine();

                    switch (inputC)
                    {
                        case "A":
                            EditPos();
                            break;
                        case "B":
                            EditRot();
                            break;
                        case "C":
                            EditScale();
                            break;
                        case "M":
                            ctrlDone = true;
                            break;
                        case "Q":
                            Console.WriteLine("Program now exiting... \n(Press any key to continue)");
                            Console.ReadLine();
                            System.Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("Invalid Key.");
                            //Console.ReadLine();
                            break;
                    }
                }
            }
        }
        //FOR OBJECT'S POSITION
        public void EditPos()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with an existing object in which you would like to position:");
                inputNum = Console.ReadLine();

                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            string newX;
            string newY;
            string newZ;

            var statsArray = objStats[tempNum].Split('\n');

            while (inputEntered2 == false)
            {
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's position on the X axis:");
                newX = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's position on the Y axis:");
                newY = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's position on the Z axis:");
                newZ = Console.ReadLine();

                if (Regex.IsMatch(newX, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$"))
                {
                    statsArray[5] = "Position: " + newX + "," + newY + "," + newZ; ;
                    Console.WriteLine("------------------");

                    inputEntered2 = true;
                }
                else
                {
                    Console.WriteLine("Atleast one of the inputs for the new coordinates is invalid." +
                        "Please enter in valid number coordinates for object's position.");
                }
            }
            foreach (object o in statsArray)
            {
                Console.WriteLine(o);
            }
            string objArrayString = JoinArray(statsArray);
            objStats[tempNum] = objArrayString;
        }
        //FOR OBJECT'S ROTATION
        public void EditRot()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with an existing object in which you would like to rotation:");
                inputNum = Console.ReadLine();

                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            string newX;
            string newY;
            string newZ;

            var statsArray = objStats[tempNum].Split('\n');

            while (inputEntered2 == false)
            {
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's rotation on the X axis:");
                newX = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's rotation on the Y axis:");
                newY = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's rotation on the Z axis:");
                newZ = Console.ReadLine();

                if (Regex.IsMatch(newX, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$"))
                {
                    statsArray[5] = "Rotation: " + newX + "," + newY + "," + newZ; ;
                    Console.WriteLine("------------------");

                    inputEntered2 = true;
                }
                else
                {
                    Console.WriteLine("Atleast one of the inputs for the new coordinates is invalid." +
                        "Please enter in valid number coordinates for object's rotation.");
                }
            }
            foreach (object o in statsArray)
            {
                Console.WriteLine(o);
            }
            string objArrayString = JoinArray(statsArray);
            objStats[tempNum] = objArrayString;
        }
        //FOR OBJECT'S SCALE
        public void EditScale()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with an existing object in which you would like to scale:");
                inputNum = Console.ReadLine();

                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            string newX;
            string newY;
            string newZ;

            var statsArray = objStats[tempNum].Split('\n');

            while (inputEntered2 == false)
            {
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's scale on the X axis:");
                newX = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's scale on the Y axis:");
                newY = Console.ReadLine();
                Console.WriteLine("\nPlease enter in the new number coordinates for the object's scale on the Z axis:");
                newZ = Console.ReadLine();

                if (Regex.IsMatch(newX, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$") && Regex.IsMatch(newY, @"^\d{1,4}([.]\d{1,2})?$"))
                {
                    statsArray[5] = "Scale: " + newX + "," + newY + "," + newZ; ;
                    Console.WriteLine("------------------");

                    inputEntered2 = true;
                }
                else
                {
                    Console.WriteLine("Atleast one of the inputs for the new coordinates is invalid." +
                        "Please enter in valid number coordinates for object's scale.");
                }
            }
            foreach (object o in statsArray)
            {
                Console.WriteLine(o);
            }
            string objArrayString = JoinArray(statsArray);
            objStats[tempNum] = objArrayString;
        }
        //FOR DELETING OBJECT
        public void DeleteObj()
        {
            bool inputEntered;
            bool inputEntered2;
            //string input;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;


            while (inputEntered == false)
            {
                if (objStats.Count < 1)
                {
                    Console.WriteLine("There are no objects present to delete. Please create an object first!");
                    CreateObj();
                }
                else
                {
                    Console.WriteLine("\nPlease enter in the number associated with an existing object in which you would like to delete:");
                    inputNum = Console.ReadLine();

                    if (Regex.IsMatch(inputNum, @"^\d$"))
                    {
                        for (int i = 0; i < objStats.Count; i++)
                        {
                            if (objStats[i].Contains("Number: " + inputNum))
                            {
                                tempNum = i;
                                inputEntered = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter in a valid object number.");
                    }
                }
            }

            var statsArray = objStats[tempNum].Split('\n');

            while (inputEntered2 == false)
            {
                Console.WriteLine("Delete " + statsArray[2] + "? (Y or N)");
                inputD = Console.ReadLine();

                if (inputD == "Y")
                {
                    objStats.RemoveAt(tempNum);
                    Console.WriteLine(statsArray[2] + " deleted.");

                    inputEntered2 = true;
                }
                else if (inputD == "N")
                {
                    DeleteObj();
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter in a valid response");
                }
            }

            int thisNum = 0;

            for (int i = 0; i < objStats.Count; i++)
            {
                thisNum++;
                var statsArray2 = objStats[i].Split('\n');
                statsArray2[0] = "Object Number: " + thisNum;

                string objArrayString = JoinArray(statsArray2);
                objStats[i] = objArrayString;
            }
        }
        //FOR EXPORTING OBJECT INFO
        public void ExportObj()
        {
            exportDone = false;

            while (exportDone == false)
            {
                if (objStats.Count < 1)
                {
                    Console.WriteLine("Please create an object first!");
                    //CreateObj();
                    exportDone = true;
                }
                else
                {
                    Console.WriteLine("\nPlease select an option: \n-------------------------------------" +
                        "\nA = Export Object Info to New Text File \nB = Export Object Info to Existing Text File " +
                        "\nM = Previous Menu \nQ = Quit Application");
                    inputF = Console.ReadLine();

                    switch (inputF)
                    {
                        case "A":
                            Write2New();
                            break;
                        case "B":
                            Write2Existing();
                            break;
                        case "M":
                            ctrlDone = true;
                            break;
                        case "Q":
                            Console.WriteLine("Program now exiting... \n(Press any key to continue)");
                            Console.ReadLine();
                            System.Environment.Exit(1);
                            break;
                        default:
                            Console.WriteLine("Invalid Key.");
                            //Console.ReadLine();
                            break;
                    }
                }
            }
        }
        //WRITING TO NEW TEXT FILE
        private void Write2New()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with the object in which you would like to export:");
                inputNum = Console.ReadLine();

                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            var statsArray = objStats[tempNum].Split('\n');

            inputEntered2 = false;

            string textFile = "";

            while (inputEntered2 == false)
            {
                Console.WriteLine("\nPlease enter in name of text file to create and export to: ");
                textFile = Console.ReadLine();

                if (textFile != null)
                {
                    inputEntered2 = true;
                }
                else
                {
                    Console.WriteLine("Please enter in a valid name for text file.");
                }
            }
            System.IO.StreamWriter file = new System.IO.StreamWriter("../../" + textFile + ".txt");

            foreach (string stats in statsArray)
            {
                file.WriteLine(stats);
            }

            file.WriteLine("\n");
            file.Close();
        }
        //WRITING TO EXISTING TEXT FILE
        private void Write2Existing()
        {
            CountObj();

            bool inputEntered;
            bool inputEntered2;
            int tempNum = 0;

            inputEntered = false;
            inputEntered2 = false;

            while (inputEntered == false)
            {
                Console.WriteLine("\nPlease enter in the number associated with the object in which you would like to export:");
                inputNum = Console.ReadLine();

                if (Regex.IsMatch(inputNum, @"^\d$"))
                {
                    for (int i = 0; i < objStats.Count; i++)
                    {
                        if (objStats[i].Contains("Number: " + inputNum))
                        {
                            tempNum = i;
                            inputEntered = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid object number.");
                }
            }

            var statsArray = objStats[tempNum].Split('\n');

            inputEntered2 = false;

            string textFile;
            string targetFile;

            while (inputEntered2 == false)
            {
                Console.WriteLine("\nPlease enter in name of existing text file to export to: ");
                textFile = Console.ReadLine();

                if (textFile != null)
                {
                    targetFile = @"../../" + textFile + ".txt";
                    if (File.Exists(targetFile))
                    {
                        Console.WriteLine("We found a match");

                        // Create a file to write to.
                        using (StreamWriter file = File.AppendText(targetFile))
                        {
                            foreach (string stats in statsArray)
                            {
                                file.WriteLine(stats);
                            }

                            file.WriteLine("\n");
                        }

                        inputEntered2 = true;
                    }
                    else
                    {
                        Console.WriteLine("Error. Invalid key or couldn't find desired file.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter in a valid name for text file.");
                }
            }
        }
        //FOR JOINING AN ARRAY OF STRINGS INTO ON STRING
        static string JoinArray(string[] array)
        {
            string result = string.Join("\n", array);
            return result;
        }
        //FOR GOING THROUGH ALL EXISTING OBJECTS AND DISPLAYING THEM
        public void CountObj()
        {
            Console.WriteLine("\n\nCurrent Objects Present: " + objStats.Count + "\n");

            foreach (object o in objStats)
            {
                Console.WriteLine(o + "\n");
            }
        }
    }
}
