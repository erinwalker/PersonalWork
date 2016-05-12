select a.FirstName, m.Title from Actors a
inner join Filmography f on a.ActorID = f.ActorID
inner join Movies m on f.MovieID = m.MovieID
