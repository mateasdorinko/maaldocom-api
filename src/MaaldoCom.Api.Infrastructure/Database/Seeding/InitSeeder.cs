using Microsoft.EntityFrameworkCore.Migrations;

namespace MaaldoCom.Api.Infrastructure.Database.Seeding;

public static class InitSeeder
{
    public static void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Knowledge",
            columns: ["Title", "Quote"],
            values: new object[,]
            {
                { "BRUTAL HONESTY", "Actually there are silly ideas." },
                { "STAMINA", "You're so far behind now you may as well stop trying." },
                { "REALISM", "It really does matter whether you win or lose." },
                { "REALITY", "Winning isn't everything, but it sure is better than losing." },
                { "INTELLIGENCE", "People tend to hold overly favorable views of their abilities in many social and intellectual domains." },
                { "CONSULTING", "If you're not part of the solution, there's good money to be made in prolonging the problem." },
                { "ELITISM", "It's lonely at the top. But it's comforting to look down upon everyone at the bottom." },
                { "DEMOTIVATION", "Sometimes the best solution to morale problems is just to fire all of the unhappy people." },
                { "LONELINESS", "If you find yourself struggling with loneliness, you're not alone. And yet you are alone. So Very Alone." },
                { "ARROGANCE", "The best leaders inspire by example. When that's not an option, brute intimidation works pretty well too." },
                { "CLUELESSNESS", "There are no stupid questions, but there are a lot of inquisitive idiots." },
                { "INCOMPETENCE", "When you earnestly believe you can compensate for a lack of skill by doubling your efforts, theres no end to what you can't do." },
                { "IGNORANCE", "It's amazing how much easier it is for a team to work together when no one has any idea where they're going." },
                { "PRETENSION", "The down side to being better than everyone else is that people tend to assume you're pretentious." },
                { "DYSFUNCTION", "The only consistent feature of all of your dissatisfying relationships is you." },
                { "IDIOCY", "Never underestimate the power of stupid people in large groups." },
                { "LAZINESS", "Success is not a journey, and not a destination. So stop running." },
                { "UNDERACHIEVEMENT", "The tallest blade of grass is the first to be cut by the lawnmower." },
                { "BURNOUT", "Attitudes are contagious. Mine might kill you." },
                { "DOUBT", "In the battle between you and the world, bet on the world." },
                { "PROCRASTINATION", "Hard work often pays off after time, but laziness always pays off now." },
                { "MEDIOCRITY", "It takes a lot less time and most people wont notice the difference until its too late." },
                { "MISTAKES", "It could be that the purpose of your life is only to serve as a warning to others." },
                { "INEPTITUDE", "If you cant learn to do something well, learn to enjoy doing it poorly." },
                { "FEAR", "Until you have the courage to lose sight of the shore, you will not know the terror of being forever lost at sea." },
                { "HAZARDS", "There is an island of opportunity in the middle of every difficulty, miss that though, and you're pretty much doomed." },
                { "IRRESPONSIBILITY", "No single raindrop believes it is to blame for the flood." },
                { "OVERCONFIDENCE", "Before you attempt to beat the odds, be sure you can survive the odds beating you." },
                { "REGRET", "It hurts to admit when you've made mistakes, but when they're big enough, the pain only last a second." },
                { "RISKS", "If you never try anything new, you'll miss out on many of life's great disappointments." },
                { "SACRIFICE", "Your role may be thankless, but if you're willing to give it your all, you just might bring success to those who outlast you." },
                { "BITTERNESS", "Never be afraid to share your dreams with the world, because theres nothing the world loves more than eating really sweet dreams." },
                { "BLAME", "The secret to success is knowing who to blame for your failures." },
                { "DARE TO SLACK", "When birds fly in the proper formation, they only need to exert half the effort. Even in nature, teamwork results in collective laziness." },
                { "DELUSIONS", "There is no joy greater than soaring high on the wings of your dreams, except maybe the joy of watching a dreamer who has nowhere to land but in the ocean of reality." },
                { "DISLOYALTY", "There comes a time when every team must learn to sacrifice individuals." },
                { "DISSERVICE", "It takes months to find a customer, but only seconds to lose one. The good news is we should run out of them in no time." },
                { "INSANITY", "Its difficult to comprehend how insane some people can be, especially when you're insane." },
                { "ADVERSITY", "That which does not kill me only postpones the inevitable." },
                { "CONFORMITY", "When people are free to do as they please, they usually imitate each other." },
                { "DESPAIR", "It's always darkest just before it goes pitch black." },
                { "HUMILIATION", "The harder you try, the dumber you look." },
                { "MISFORTUNE", "While good fortune often eludes you, misfortune never misses." },
                { "PROBLEMS", "No matter how great and destructive your problems may seem now, remember... you've probably only seen the tip of them." },
                { "TROUBLE", "Luck cant last a lifetime, unless you die young." },
                { "AGONY", "Not all pain is gain." },
                { "DEFEAT", "For every winner, there are dozens of losers. Odds are you're one of them." },
                { "FAILURE", "When your best just isn't good enough." },
                { "FUTILITY", "You'll always miss 100% of the shots you don't take, and, statistically speaking, 99% of the shots that you do." },
                { "LOSING", "If at first you dont succeed, failure may be your style." },
                { "PESSIMISM", "Every dark cloud has a silver lining, but lightning kills hundreds of people each year who are trying to find it." },
                { "STUPIDITY", "Quitters never win, Winners never quit, but those that never win AND never quit are idiots." } ,
                { "DEEP THOUGHTS", "I've learned that you shouldn't compare yourself to others, they are more screwed up than you think." },
                { "DEEP THOUGHTS", "I've learned that you cannot make someone love you. All you can do is stalk them and hope they panic and give in." },
                { "DEEP THOUGHTS", "I've learned that you can keep puking long after you think you're finished." },
                { "DEEP THOUGHTS", "I've learned that we are responsible for what we do, unless we are celebrities." },
                { "DEEP THOUGHTS", "I've learned that regardless of how hot and steamy a relationship is at first, the passion fades, and there had better be a lot of money to take it's place." },
                { "DEEP THOUGHTS", "I've learned that the people you care most about in life are taken from you too soon and all the less important ones just never go away." },
                { "ALL ABOUT ME", "When I grow up I want to be just like me." },
                { "AMBITION", "Some people dream of success, while other people get up every morning and make it happen... or at least try to make it happen before they hit the snooze button." },
                { "ATTITUDE", "If you think you're perfect, you probably are. Everyone else thinks so too." },
                { "AUTHORITY", "The key to successful leadership is influence, not authority... but a little authority helps." },
                { "BUSINESS", "The first rule of business is: Do whatever it takes to get the sale. The second rule of business is: See rule number one." },
                { "CHANGE", "The only thing that is constant is change... except for my opinion, which is always right." },
                { "COMMUNICATION", "The single biggest problem in communication is the illusion that it has taken place." },
                { "COMPETITION", "The best way to beat the competition is to stop trying to beat the competition." },
                { "DISAPPROVAL", "The more you disapprove, the more fun it is for me" },
                { "FLATTERY", "If you want to get to the top, be prepared to kiss a lot of the bottom." },
                { "GET TO WORK", "You aren't being paid to believe in the power of your dreams." },
                { "GOALS", "It's to avoid standing directly between a competitive jerk and their goals" },
                { "INDIFFERENCE", "It takes 43 muscles to frown and 17 to smile, but it doesn't take any to just sit there with a dumb look on your face." },
                { "INDIVIDUALITY", "Always remember that you are unique. Just Like Everybody Else." },
                { "LIMITATIONS", "Until you spread your wings, You'll have no idea how far you can walk." },
                { "MEETINGS", "None of us is as dumb as all of us together." },
                { "MOTIVATION", "If a pretty poster and a cute saying are all it takes to motivate you, you probably have a very easy job. The kind robots will do soon." },
                { "PLANNING", "Much work remains to be done before we can announce our total failure to make any progress." },
                { "STRIFE", "As long as we have each other, we'll never run out of problems." },
                { "COMPUTER LOVE", "Hey you're kind of cute. Ever consider a relationship with a computer?" },
                { "FRIENDSHIP", "A good friend will come and bail you out of jail, but a true friend will be sitting next to you saying: Gosh, that was fun." }
            });

        migrationBuilder.InsertData(
            table: "Tags",
            columns: ["Name"],
            values: new object[,]
            {
                { "hotshots" },
                { "family" },
                { "vacations" },
                { "birthdays" },
                { "halloween" },
                { "weddings" },
                { "matt" },
                { "heather" },
                { "hunter" },
                { "austin" }
            });
    }

    public static void Down(MigrationBuilder migrationBuilder)
    {
        throw new NotImplementedException();
    }
}
