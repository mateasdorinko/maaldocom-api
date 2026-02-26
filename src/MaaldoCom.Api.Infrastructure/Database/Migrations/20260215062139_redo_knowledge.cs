using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaldoCom.Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class redo_knowledge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Knowledge WHERE 1=1");

            migrationBuilder.InsertData(
                table: "Knowledge",
                columns: ["Title", "Quote"],
                values: new object[,]
                {
                    { "KNOWLEDGE IS POWER", "Knowledge is power. Unfortunately, it doesn’t come with a user manual." },
                    { "GOOGLE DEGREE", "I have a PhD in Googling things five minutes before I need them." },
                    { "CONFIDENTLY WRONG", "Nothing builds confidence like being completely wrong with enthusiasm." },
                    { "LIFELONG LEARNING", "I’m not getting older — I’m leveling up my experience points." },
                    { "WISDOM TAX", "Experience is what you get right after you needed it." },
                    { "BOOK SMARTS", "I read books. Sometimes I even finish them." },
                    { "HALF FACTS", "A little knowledge is dangerous. A lot of knowledge is suspicious." },
                    { "BRAIN CACHE", "My brain has too many tabs open and one of them is playing music." },
                    { "OVERTHINKING", "I don’t overthink. I just thoroughly explore every possible disaster." },
                    { "FACT CHECK", "Trust but verify. Then verify the verification." },
                    { "CURIOUS MIND", "Curiosity killed the cat, but satisfaction brought it back with better questions." },
                    { "DATA DRIVEN", "In data we trust. In spreadsheets we panic." },
                    { "COMMON SENSE", "Common sense is like deodorant — the people who need it most don’t use it." },
                    { "TRIAL AND ERROR", "Trial and error is just science with better branding." },
                    { "EXPERT MODE", "An expert is someone who made all the mistakes before you did." },
                    { "YOUTUBE UNIVERSITY", "If YouTube taught me anything, it’s that I can fix it. Probably." },
                    { "THINKING CAP", "I put on my thinking cap. It was just a helmet for bad ideas." },
                    { "LEARNING CURVE", "The learning curve is just a polite way of saying ‘brace yourself.’" },
                    { "FACTS AND FEELINGS", "Facts don’t care about feelings, but feelings definitely don’t care about facts." },
                    { "SMART ENOUGH", "I know enough to be dangerous, but not enough to be safe." },
                    { "HINDSIGHT HERO", "In hindsight, I am a flawless genius." },
                    { "DEBUGGING LIFE", "Life is just debugging a problem you didn’t write." },
                    { "STUDY HARD", "Study hard now so future you can pretend it was easy." },
                    { "ASK WHY", "Always ask why. Then ask who broke it." },
                    { "PROCRASTINATION THEORY", "I don’t procrastinate. I gather data under pressure." },
                    { "SCIENCE FAIR", "Science is organized curiosity with better lighting." },
                    { "KNOW IT ALL", "I don’t know it all. I just know where to look." },
                    { "HUMBLE PIE", "Every mistake is a slice of humble pie. I’m well fed." },
                    { "WIKI WARRIOR", "I came. I saw. I cited Wikipedia." },
                    { "FOCUS MODE", "I have laser focus — like a cat chasing a reflection." },
                    { "THE FINE PRINT", "Wisdom lives in the fine print nobody reads." },
                    { "GUESSING GAME", "Sometimes confidence is just guessing with good posture." },
                    { "SMARTER TOMORROW", "Learn something today so tomorrow you can sound impressive." },
                    { "BRAIN UPGRADE", "My brain needs a firmware update." },
                    { "FAIL FORWARD", "Failure is just research you weren’t planning on publishing." },
                    { "QUESTION EVERYTHING", "Question everything — especially your own conclusions." },
                    { "QUICK FIX", "If at first you don’t succeed, redefine success." },
                    { "NIGHT BEFORE TEST", "Nothing accelerates learning like a test tomorrow." },
                    { "MEMORY LEAK", "I remember useless facts forever and important ones never." },
                    { "DEEP THOUGHTS", "I think deeply. Mostly about snacks." },
                    { "INFORMATION OVERLOAD", "Too much information is just trivia with ambition." },
                    { "SMART WORK", "Work smarter, not harder — unless you forgot to plan." },
                    { "EXTRA CREDIT", "I asked for extra credit. Life handed me extra lessons." },
                    { "FACT HUNTER", "I hunt facts like treasure. Sometimes I dig up opinions." },
                    { "INTELLIGENCE TEST", "The real test of intelligence is knowing when to stop talking." },
                    { "OVERQUALIFIED", "I’m overqualified in random knowledge." },
                    { "FOOLISH CONFIDENCE", "Confidence is silent. Insecurity gives PowerPoint presentations." },
                    { "NOTE TO SELF", "Write it down. Your memory is a rumor." },
                    { "RESEARCH MODE", "I research everything. Including why I research everything." },
                    { "SMARTER THAN YESTERDAY", "Be smarter than yesterday. Yesterday made some choices." },
                    { "QUICK LEARNER", "I’m a quick learner once I finally start." },
                    { "FACTS FIRST", "Facts first. Panic later." },
                    { "READ THE MANUAL", "I read the manual after the smoke." },
                    { "IDEA FACTORY", "My brain is an idea factory with questionable quality control." },
                    { "KNOWLEDGE DIET", "Feed your brain. It burns calories thinking about pizza." },
                    { "CONFUSED BUT TRYING", "I may be confused, but I’m learning aggressively." },
                    { "ALMOST RIGHT", "I was almost right. Which is not the same as right." },
                    { "SMART ENOUGH TO ASK", "Smart isn’t knowing everything. It’s knowing who to ask." },
                    { "FACT CHECK YOURSELF", "Before correcting others, update your own software." },
                    { "BRAIN BUFFERING", "Please wait. Brain buffering." },
                    { "LATE NIGHT GENIUS", "My best ideas arrive after my discipline leaves." },
                    { "DATA NOT DRAMA", "Choose data over drama. It’s quieter." },
                    { "HARD LESSONS", "Some lessons are expensive. Tuition varies." },
                    { "OVERPREPARED", "I prepared for everything except what happened." },
                    { "STILL LEARNING", "If you’re the smartest in the room, find another room." },
                    { "FACT FUEL", "Knowledge is fuel. Use it before it expires." },
                    { "WITNESS PROTECTION", "I hide my ignorance in plain sight." },
                    { "BRAIN GYM", "Reading is lifting weights for your brain." },
                    { "QUESTION MARK", "A question mark is the most powerful punctuation." },
                    { "SMARTER NOT LOUDER", "Being loud isn’t the same as being right." },
                    { "UNEXPECTED LESSONS", "Life teaches pop quizzes without study guides." },
                    { "STAY CURIOUS", "Curiosity keeps the mind young and occasionally confused." },
                    { "THINK BEFORE SEND", "Think before you hit send. Or learn publicly." },
                    { "RANDOM FACTS", "I collect random facts like trophies nobody asked for." },
                    { "CLARITY MOMENT", "Clarity arrives right after embarrassment." },
                    { "UPGRADE COMPLETE", "I upgraded my knowledge. Still working on wisdom." },
                    { "SMART HUMILITY", "The smarter you get, the more you realize you’re not." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
