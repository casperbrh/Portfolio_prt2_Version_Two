using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DataService
    {
        /*public List<Post> GetPosts()
        {
            using (var db = new SOVAContext())


                return db.Posts.ToList();
        }*/
        //Full post including comments and tags
        //Get post by comment parentid. Only way we can find that gets question 
        //with comment and posttags
        /*public Post GetPost(int id)
        {
            using (var db = new SOVAContext())
            {
                var fullPost = db.Posts
                    .Where(x => x.PostId == id)
                    .Include(x => x.PostTags)
                    .FirstOrDefault();
                    
                    
                    //.FirstOrDefault(x => x.PostId == id);
                return fullPost;
            }
        }*/
       
            

        //Questions
        //edited
        public List<Question> GetQuestions(int page, int pageSize)
        {
            using (var db = new SOVAContext())
            {
                var questions = db.Questions
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
                    
                return questions;
            }
        }

        /* public Post GetQuestion(int id)
         {
             using (var db = new SOVAContext())
             {
                 var question = db.Posts
                     .Where(x => x.ParentId == null)
                     .FirstOrDefault(x => x.PostId == id);
                 return question;
             }
         }*/
        //edited

        public Question GetQuestion(int id) //Delete (.Include(x => x.Comments) ) because it couldnt work.
        {
            using (var db = new SOVAContext())
            {
                var question = db.Questions
                   // .Include(x => x.Comments)
                    .FirstOrDefault(x => x.Id == id);
                return question;
            }
        }


        public SearchHistories SearchHistories(int id)
        {
            using (var db = new SOVAContext())
            {
                var question = db.SearchHistory
                    //.Include(x => x.User)
                    .Where(x => x.UserId == id)
                    .FirstOrDefault();
                return question;
            }
        }
        //Comments
        public List<Comment> GetQuestionComments(int id)
        {
            using (var db = new SOVAContext())
            {

                var commentsToQuestion = db.Comments
                      
                      .Where(x => x.PostId == id)
                      .ToList();
                return commentsToQuestion;
            }
        }

        public List<Comment> GetCommentsByAnswer(int id)
        {
            using (var db = new SOVAContext())
            {
                var answercomment = db.Comments
                    .Where(x => x.PostId == id)
                    .ToList();
                return answercomment;
            }

        }

        /*public Question GetQuestionComments(int id)
        {
            using (var db = new SOVAContext())
            {
                var questioncomment = db.Questions
                    .Include(x => x.Comments)
                    .FirstOrDefault(x => x.Id == id);
                return questioncomment;
            }
        }*/
        //need to edit
        public List<Question> GetQuestionsByString(string title, int page, int pageSize)
        {
            using (var db = new SOVAContext())
            {
                var question = db.Questions
                .Where(x => (x.Body.ToLower().Contains(title.ToLower()) || x.Title.ToLower().Contains(title.ToLower())));
                return question
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

            }
        }

        //Answers
        //edited
        public List<Answer> GetAnswers()
        {
            using (var db = new SOVAContext())
            {
                var answers = db.Answers.ToList();
                    
                return answers;
            }
        }
        //Edited
        public Answer GetAnswer(int id)
        {
            using (var db = new SOVAContext())
            {
                var answer = db.Answers
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
                    //.FirstOrDefault(x => x.PostId == id);

                return answer;
            }
        }
        public List<Answer> GetAnswersByParent(int id)
        {
            using (var db = new SOVAContext())
            {
                var answerbyparent = db.Answers
                    .Where(x => x.ParentId == id);
                return answerbyparent.ToList();
            }
        }

        public int GetNumberOfAnswers()
        {
            using (var db = new SOVAContext())
            {
                return db.Answers.Count();
            }
        }
        public int GetNumberOfQuestions()
        {
            using (var db = new SOVAContext())
            {
                return db.Questions.Count();
            }
        }
        //-------------------------------users----------------
        public List<User> GetUsers()
        {
            using (var db = new SOVAContext())
            {
                var userss = db.Users
                    .ToList();
                return userss;
            }
        }

        public User GetUser(int id)
        {
            using (var db = new SOVAContext())
            {
                var user = db.Users
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
                return user;
            }
        }


        public User createUser(string name, string password)
        {
            using (var db = new SOVAContext())
            {


                var creationdate = DateTime.Now;
                var newUser = new User()
                {

                    UserName = name,
                    Password = password,
                    CreationDate = creationdate
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser;


            }
        }

        public bool DeleteUser(int id)
        {

            try
            {
                using (var db = new SOVAContext())
                {
                    var deluser = new User()
                    {
                        Id = id
                    };
                    db.Users.Remove(deluser);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        // when update is run the createtionDate is updated to. Need to be fixed. 
        public bool UpdateUser(int userId, string newName, string newPassword)
        {
            using (var db = new SOVAContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    user.UserName = newName;
                    user.Password = newPassword;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        //SaveSearchs
        public SearchHistories SaveSearch(string newSearch, int newUserId)
        {
            using (var db = new SOVAContext())
            {
                var currentDate = DateTime.Now;
                var newSearchHistory = new SearchHistories()
                {
                    Search = newSearch,
                    UserId = newUserId,
                    Date = currentDate
                };
                db.SearchHistory.Add(newSearchHistory);
                db.SaveChanges();
                return newSearchHistory;
            }
        }

        public Annotations CreateAnnotation(string body, int userid, int postid)
        {
            using (var db=new SOVAContext())
            {   
                
                var newannotation = new Annotations()
                {
                    Body = body,
                    UserId = userid,
                    PostId = postid
                    
                };
                db.Annotations.Add(newannotation);
                db.SaveChanges();
                return newannotation;
            }
        }

        public bool UpdateAnnotation(string body, int userId, int postid)
        {
            using (var db = new SOVAContext())
            {
                var anno = db.Annotations.FirstOrDefault(x => x.UserId == userId);
                if (anno != null)
                {
                    anno.UserId = userId;
                    anno.PostId = postid;
                    anno.Body = body;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool DeleteAnnotation(int userid,int postid, string body)
        {
            try
            {
                using (var db = new SOVAContext())
                {
                    var delannotation = new Annotations()
                    {
                       UserId=userid,
                       PostId=postid,
                       Body = body
                    };
                    db.Annotations.Remove(delannotation);
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
          
            
        public Mark CreateMarking(int postid, int userid)
        {
            using (var db=new SOVAContext())
            {
                var newmark = new Mark()
                {
                    PostId=postid,
                    UserId=userid
                };
                db.Marked.Add(newmark);
                db.SaveChanges();
                return newmark;
            }
        }

        public bool DeleteMarking(int postid, int userid)
        {
            try
            {
                using (var db = new SOVAContext())
                {
                    var delmarking = new Mark()
                    {
                        PostId = postid,
                        UserId = userid
                    
                      
                    };
                    db.Marked.Remove(delmarking);
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
