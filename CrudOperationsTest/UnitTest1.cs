namespace CrudOperationsTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var actual = Add(2, 2);
            Assert.Equal(4, actual);
        }

        [Fact]
        public void Test2()
        {
            var actual = Add(2, 2);
            Assert.Equal(4, actual);
        }


        int Add(int x , int y)
        {
            return x + y;   
        }



    }
}