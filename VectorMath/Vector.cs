namespace VectorMath
{
    public struct Vector
    {

        private float _x;
        public float _y;
        public float _z;
        
        public string Name { get; set; }
        public float X => _x;
        public float Y => _y;
        public float Z => _z;

        //multipurpose constructor
        public Vector(string name = null, float x = 0, float y = 0, float z = 0)
        {
            Name = name;
            _x = x;
            _y = y;
            _z = z;
        }

        public void SetVectorCoordinates(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        

        //SqrMagnitude
        public float SquareMagnitude()
        {
            return _x * _x + _y * _y + _z * _z;
        }

        //Length 
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
            => new Vector("VectorSum",v1._x + v2._x, v1._y + v2._y, v1._z + v2._z);
        //Subtract
        public static Vector operator -(Vector v1, Vector v2)
            => new Vector("VectorDifference",v1._x - v2._x, v1._y - v2._y, v1._z - v2._z);
        //Multiply
        public static Vector operator *(Vector v1, float scalar)
            => new Vector("VectorProduct",v1._x * scalar, v1._y * scalar, v1._z * scalar);
        
        
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