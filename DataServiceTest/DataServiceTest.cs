using DataLayer;
using System;
using System.Linq;
using Xunit;


namespace DataServiceTest
{
    public class DataServiceTest
    {
    
        //questions
        [Fact]
        public void GetQuestions()
        {
            var service = new DataService();
            var posts = service.GetQuestions(0 , 10);
            Assert.Equal(10, posts.Count);
            Assert.Equal("Hide Start Menu and Start Button in VB.NET", posts.First().Title);

        }

        [Fact]
        public void GetQuestions_ByValidId()
        {
            var service = new DataService();
            var posts = service.GetQuestion(13649012);
            DateTime myDate = DateTime.ParseExact("2012-11-30 16:21:10", "yyyy-MM-dd HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
            Assert.Equal(myDate,posts.CreationDate);
        }

        //GetQuestion_ByString
        /*[Fact]
        public void GetQuestions_ByString_ReturnsList()
        {
            var service = new DataService();
            var questions = service.GetQuestionsByString("Hide", 0, 5);
            Assert.Equal(30, questions.Count);
            Assert.Equal("Hide Start Menu and Start Button in VB.NET", questions.First().Title);
        }*/
     
        //Answers
        [Fact]
        public void GetAnswers()
        {
            var service = new DataService();
            var posts = service.GetAnswers();
            Assert.Equal(11392, posts.Count);
        }

        /*[Fact]
        public void GetAnswer_WithComments()
        {
            var service = new DataService();
            var answers = service.GetAnswer(12713875);
            Assert.Equal(2, answers.Comments.Count());
        }*/




        /* [Fact]
        public void GetAnswer_Accepted()
        {
            var service = new DataService();
            var answer = service.GetAcceptedAnswer(24362641);
            Assert.Equal(24361884, answer.Id);
        }*/


      /*  [Fact]
        public void GetAnswers_ByValidId()
        {
            var service = new DataService();
            var posts = service.GetAnswer(3126560);
            Assert.Equal(2180354, posts.ParentId);
            Assert.Equal(1, posts.Score);
        }*/

        //comments

       /* [Fact]
        public void GetCommentsToAQuestion()
        {
            var service = new DataService();
            var comments = service.GetQuestionComments(13649012);
            DateTime myDate = DateTime.ParseExact("2012-11-30 16:53:35", "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
            Assert.Equal(myDate,comments.FirstOrDefault().CreationDate);
        }*/

        [Fact]
        public void CreateNewUserTest()
        {
            var service = new DataService();
            var userNew = service.createUser("Henning", "Flemming");
           
        }

        [Fact]
        public void DeleteUser()
        {
            var service = new DataService();
        
            var users = service.createUser("MOther", "father").Id;

            var deluser = service.DeleteUser(users);
            Assert.True(deluser);

        }

        [Fact]
        public void UpdateUser_userName_Password_WithValidId()
        {
            var service = new DataService();
            var newUser = service.createUser("userUpdateTest", "testertest");
            

            var user = service.UpdateUser(newUser.Id,"updatedusername","updatedpassword");
            Assert.True(user);

            newUser= service.GetUser(newUser.Id);
            Assert.Equal("updatedusername", newUser.UserName);
            Assert.Equal("updatedpassword", newUser.Password);

            service.DeleteUser(newUser.Id);
        }
       
        [Fact]
        public void UpdateUser_InvalidId_ReturnsFalse()
        {
            var service = new DataService();
            var userr = service.UpdateUser(-10,"updatedusername","updatedpassword");
            Assert.False(userr);
        }

        [Fact]
        public void SaveUserSearchHistory()
        {
            var service = new DataService();
            var newUser = service.createUser("History","Saved");
            var newSearch = service.SaveSearch("Hide",newUser.Id);
            Assert.Equal("Hide", newSearch.Search);
        }

        //--------------Annotation test----------------
        [Fact]
        public void CreateAnnotation()
        {
            var service = new DataService();
            var newUser = service.createUser("Annotationtor","Annobreaker").Id;
            var parentIdentfire = service.GetQuestion(13649012).Id;
            var newAnnotation = service.CreateAnnotation("Annotation_created",newUser,parentIdentfire);
            Assert.Equal("Annotation_created", newAnnotation.Body);

            //Clean up
            //deleteuser

            
        }

        [Fact]
        public void UpdateAnnotations_Body_Userid_Postid()
        {
            var service = new DataService();
            var newannotation = service.UpdateAnnotation("Annotation is updated now", 99, 13649012);
            Assert.True(newannotation);
            //Assert.Equal("Annotation is updated", );
        }

        //---Needs to be fixed
        [Fact]
        public void DeleteAnnotation()
        {
            var service = new DataService();
            var newuser= service.createUser("Camie", "cami123").Id;
            var newpost = service.GetAnswer(9854666).Id;
            var newannotation = service.CreateAnnotation("Annotation to be deleted",newuser,newpost);
            var delannotation = service.DeleteAnnotation(newannotation.UserId, newannotation.PostId, newannotation.Body);
            Assert.True(delannotation);

        }
        


       //-----------------Marking Test-----------------
        [Fact]
        public void CreateMark()
        {
            var service = new DataService();
            var newuser = service.createUser("marker","mark123").Id;
            var parentpostt = service.GetQuestion(13649012).Id;
            var newMarking = service.CreateMarking(parentpostt,newuser);
            Assert.Equal(13649012, newMarking.PostId);

            //delete user
        }

        [Fact]
        public void DeleteMarking()
        {
            var service = new DataService();
            var newuser = service.createUser("Peter", "pet123").Id;
            var postt = service.GetQuestion(13649012).Id;
            var newmark = service.CreateMarking(postt,newuser);
            var delmark = service.DeleteMarking(newmark.PostId, newmark.UserId);
            Assert.True(delmark);

        }
    } //closening the DataService class

} //closing the namespace DataServiceTest 
