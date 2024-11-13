namespace VectorMath
{
    public struct Vector
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        
        // public Vector(string name, float x, float y, float z)
        // {
        //     Name = name;
        //     X = x;
        //     Y = y;
        //     Z = z;
        // }
        //
        // public Vector()
        // {
        //     Name = null;
        //     X = 0;
        //     Y = 0;
        //     Z = 0;
        // }
        //
        // public Vector(string name)
        // {
        //     Name = name;
        //     X = 0;
        //     Y = 0;
        //     Z = 0;
        // }

        //multipurpose constructor
        public Vector(string name = null, float x = 0, float y = 0, float z = 0)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
        }
        

        //SqrMagnitude
        public float SquareMagnitude()
        {
            return X * X + Y * Y + Z * Z;
        }

        //Lenght 
        public float Length()
        {
            return MathF.Sqrt(SquareMagnitude());
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
        
    
        //calculation
        public static Vector operator +(Vector v1, Vector v2)
            => new Vector("VectorSum",v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        //Subtract
        public static Vector operator -(Vector v1, Vector v2)
            => new Vector("VectorDifference",v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    
        //Multiply
        public static Vector operator *(Vector v1, float scalar)
            => new Vector("VectorProduct",v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        
        //Distance
        public static float Distance(Vector v1, Vector v2)
        {
            Vector direction = v1 - v2;
            return direction.Length();
        }
        public float Distance(Vector otherVector)
        {
            Vector direction = this - otherVector;
            return direction.Length();

        }
    }
}