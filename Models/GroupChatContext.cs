using Microsoft.EntityFrameworkCore;
using System;
namespace GroupChatApp.Models
{
    public class GroupChatContext:DbContext
    {
        public GroupChatContext(DbContextOption<GroupChatContext>options):base(options)
        {

        }
        public DbSet<Group> Groups{get;set;}
        public DbSet<Message> Messages{get;set;}
        public DbSet<UserGroup> UserGroups{get;set;}
    }
}