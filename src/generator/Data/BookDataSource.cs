﻿using System.Text.Json;

namespace Generator.Data
{
    internal class BookDataSource: IEntityDataSource<Book>
    {
        #region Raw data

        private const string RawData =
            """
            [{
                    "name": "The Time Machine",
                    "description": "A man travels through time and witnesses the evolution of humanity.",
                    "author": "H.G. Wells",
                    "year": 1895
                }, {
                    "name": "Ender's Game",
                    "description": "A young boy is trained to become a military leader in a war against an alien race.",
                    "author": "Orson Scott Card",
                    "year": 1985
                }, {
                    "name": "Brave New World",
                    "description": "A dystopian society where people are genetically engineered and conditioned to conform to a strict social hierarchy.",
                    "author": "Aldous Huxley",
                    "year": 1932
                }, {
                    "name": "The Hitchhiker's Guide to the Galaxy",
                    "description": "A comedic science fiction series following the misadventures of an unwitting human and his alien friend.",
                    "author": "Douglas Adams",
                    "year": 1979
                }, {
                    "name": "Dune",
                    "description": "A desert planet is the site of political intrigue and power struggles.",
                    "author": "Frank Herbert",
                    "year": 1965
                }, {
                    "name": "Foundation",
                    "description": "A mathematician develops a science to predict the future of humanity and works to save civilization from collapse.",
                    "author": "Isaac Asimov",
                    "year": 1951
                }, {
                    "name": "Snow Crash",
                    "description": "A futuristic world where the internet has evolved into a virtual reality metaverse.",
                    "author": "Neal Stephenson",
                    "year": 1992
                }, {
                    "name": "Neuromancer",
                    "description": "A hacker is hired to pull off a near-impossible hack and gets pulled into a web of intrigue.",
                    "author": "William Gibson",
                    "year": 1984
                }, {
                    "name": "The War of the Worlds",
                    "description": "A Martian invasion of Earth throws humanity into chaos.",
                    "author": "H.G. Wells",
                    "year": 1898
                }, {
                    "name": "The Hunger Games",
                    "description": "A dystopian society where teenagers are forced to fight to the death in a televised spectacle.",
                    "author": "Suzanne Collins",
                    "year": 2008
                }, {
                    "name": "The Andromeda Strain",
                    "description": "A deadly virus from outer space threatens to wipe out humanity.",
                    "author": "Michael Crichton",
                    "year": 1969
                }, {
                    "name": "The Left Hand of Darkness",
                    "description": "A human ambassador is sent to a planet where the inhabitants are genderless and can change gender at will.",
                    "author": "Ursula K. Le Guin",
                    "year": 1969
                }, {
                    "name": "The Three-Body Problem",
                    "description": "Humans encounter an alien civilization that lives in a dying system.",
                    "author": "Liu Cixin",
                    "year": 2008
                }, {
                    "name": "2001: A Space Odyssey",
                    "description": "A monolith discovered on the Moon hints at the existence of extraterrestrial life, setting off a voyage to Jupiter.",
                    "author": "Arthur C. Clarke",
                    "year": 1968
                }, {
                    "name": "I, Robot",
                    "description": "A collection of stories detailing the evolution of robots and their relationship with humans.",
                    "author": "Isaac Asimov",
                    "year": 1950
                }, {
                    "name": "Blade Runner (Do Androids Dream of Electric Sheep?)",
                    "description": "In a post-apocalyptic world, a bounty hunter is tasked with 'retiring' rogue androids.",
                    "author": "Philip K. Dick",
                    "year": 1968
                }, {
                    "name": "Ringworld",
                    "description": "Explorers discover a massive artificial ring around a star, and delve into its mysteries.",
                    "author": "Larry Niven",
                    "year": 1970
                }, {
                    "name": "Red Mars",
                    "description": "The first of a trilogy detailing the colonization and terraforming of Mars.",
                    "author": "Kim Stanley Robinson",
                    "year": 1992
                }, {
                    "name": "Altered Carbon",
                    "description": "In a future where consciousness can be transferred between bodies, a soldier investigates a rich man's apparent suicide.",
                    "author": "Richard K. Morgan",
                    "year": 2002
                }, {
                    "name": "Hyperion",
                    "description": "Seven pilgrims share their stories as they journey to the distant world of Hyperion.",
                    "author": "Dan Simmons",
                    "year": 1989
                }, {
                    "name": "Old Man's War",
                    "description": "Elderly citizens are recruited to fight in an interstellar war, getting rejuvenated bodies in the process.",
                    "author": "John Scalzi",
                    "year": 2005
                }, {
                    "name": "Oryx and Crake",
                    "description": "A post-apocalyptic tale of biotechnological advancements and their consequences.",
                    "author": "Margaret Atwood",
                    "year": 2003
                }, {
                    "name": "Stranger in a Strange Land",
                    "description": "A human raised by Martians returns to Earth, challenging its society with his Martian values.",
                    "author": "Robert A. Heinlein",
                    "year": 1961
                }, {
                    "name": "Starship Troopers",
                    "description": "In the future, a young infantryman serves in a war against an alien species.",
                    "author": "Robert A. Heinlein",
                    "year": 1959
                }, {
                    "name": "A Wrinkle in Time",
                    "description": "Children traverse space-time to save their father and battle against dark forces.",
                    "author": "Madeleine L'Engle",
                    "year": 1962
                }, {
                    "name": "Solaris",
                    "description": "Scientists on a space station study a sentient oceanic planet.",
                    "author": "Stanisław Lem",
                    "year": 1961
                }, {
                    "name": "Fahrenheit 451",
                    "description": "In a future society, books are banned and 'firemen' burn any they find.",
                    "author": "Ray Bradbury",
                    "year": 1953
                }, {
                    "name": "Childhood's End",
                    "description": "Aliens oversee Earth's transition to a higher form of existence.",
                    "author": "Arthur C. Clarke",
                    "year": 1953
                }, {
                    "name": "The Man in the High Castle",
                    "description": "An alternate history where the Axis powers won WWII.",
                    "author": "Philip K. Dick",
                    "year": 1962
                }, {
                    "name": "Cryptonomicon",
                    "description": "The intertwined tales of WWII codebreakers and modern-day techies chasing buried treasure.",
                    "author": "Neal Stephenson",
                    "year": 1999
                }, {
                    "name": "The Moon is a Harsh Mistress",
                    "description": "A lunar colony's revolt against rule from Earth.",
                    "author": "Robert A. Heinlein",
                    "year": 1966
                }, {
                    "name": "Ubik",
                    "description": "Reality becomes elusive in this twisty, futuristic tale.",
                    "author": "Philip K. Dick",
                    "year": 1969
                }, {
                    "name": "A Canticle for Leibowitz",
                    "description": "A monastery keeps the remnants of scientific knowledge alive through a post-apocalyptic age.",
                    "author": "Walter M. Miller Jr.",
                    "year": 1960
                }, {
                    "name": "The Road",
                    "description": "A father and son traverse a post-apocalyptic world.",
                    "author": "Cormac McCarthy",
                    "year": 2006
                }, {
                    "name": "The Stars My Destination",
                    "description": "Revenge drives a man in this futuristic retelling of The Count of Monte Cristo.",
                    "author": "Alfred Bester",
                    "year": 1956
                }, {
                    "name": "Gateway",
                    "description": "An ancient alien space station with portals to distant parts of the universe becomes humanity's risky lottery ticket.",
                    "author": "Frederik Pohl",
                    "year": 1977
                }, {
                    "name": "Pandora's Star",
                    "description": "The opening of a space gate reveals a universe of wonders – and dangers.",
                    "author": "Peter F. Hamilton",
                    "year": 2004
                }, {
                    "name": "Use of Weapons",
                    "description": "A chronicle of a man who has done too much in service of an interstellar utopia.",
                    "author": "Iain M. Banks",
                    "year": 1990
                }, {
                    "name": "Pattern Recognition",
                    "description": "A hunt for the origins of a mysterious film in the digital age.",
                    "author": "William Gibson",
                    "year": 2003
                }, {
                    "name": "The Postman",
                    "description": "A lone wanderer restores hope in post-apocalyptic America.",
                    "author": "David Brin",
                    "year": 1985
                }, {
                    "name": "The Windup Girl",
                    "description": "Bioengineered creatures and a future Thailand collide in this tale of environmental and economic crises.",
                    "author": "Paolo Bacigalupi",
                    "year": 2009
                }, {
                    "name": "Revelation Space",
                    "description": "Space archaeologists uncover deadly secrets in this space opera.",
                    "author": "Alastair Reynolds",
                    "year": 2000
                }, {
                    "name": "Consider Phlebas",
                    "description": "A shapeshifting agent's quest during a galaxy-spanning war.",
                    "author": "Iain M. Banks",
                    "year": 1987
                }, {
                    "name": "Kitchen Confidential",
                    "description": "An insider's view into the culinary world, delving into the chaotic, adrenaline-fueled underbelly of restaurants.",
                    "author": "Anthony Bourdain",
                    "year": 2000
                }, {
                    "name": "Like Water for Chocolate",
                    "description": "A tale where each chapter is a month in the life of the protagonist and is preceded by a recipe. Food and love intermingle in this passionate novel.",
                    "author": "Laura Esquivel",
                    "year": 1989
                }, {
                    "name": "The Omnivore's Dilemma",
                    "description": "A comprehensive look into the modern food chain, questioning what we should eat and why.",
                    "author": "Michael Pollan",
                    "year": 2006
                }, {
                    "name": "Pride and Prejudice",
                    "description": "The story of Elizabeth Bennet as she navigates issues of manners, upbringing, morality, education, and marriage in the British landed gentry of the early 19th century.",
                    "author": "Jane Austen",
                    "year": 1813
                }, {
                    "name": "The Notebook",
                    "description": "An enduring love story about an elderly man who reads his aging wife their life story from a notebook.",
                    "author": "Nicholas Sparks",
                    "year": 1996
                }, {
                    "name": "Eat, Pray, Love",
                    "description": "Following a divorce, the author embarks on a journey around the world that becomes a quest for self-discovery.",
                    "author": "Elizabeth Gilbert",
                    "year": 2006
                }
            ]
            """;

        #endregion

        public IReadOnlyCollection<Book> Get()
        {
            return JsonSerializer.Deserialize<List<Book>>(RawData) ??
                throw new InvalidOperationException("Failed to deserialize book data");
        }
    }
}
