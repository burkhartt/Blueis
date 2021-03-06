﻿using System.Collections.Generic;
using Cachacus.Repositories;

namespace Cachacus.Tests {
    internal class CachedPersonRepository : AbstractCacheRepository<Person>, IPersonRepository {
        private readonly IPersonRepository repo;

        public CachedPersonRepository(IPersonRepository repo) {
            this.repo = repo;
        }

        public IEnumerable<Person> GetPeopleNamed(string name) {
            return GetAllByKey(name, () => repo.GetPeopleNamed(name));
        }

        public Person GetPersonWithId(int id) {
            return GetByKey(id, () => repo.GetPersonWithId(id));
        }

        protected override IEnumerable<Person> WarmUp() {
            return new[] {
                new Person("Joe", "Ramsey", 3),
                new Person("Michael", "Jordan", 4),
                new Person("Babe", "Ruth", 5)
            };
        }

        public IEnumerable<Person> GetAllPeople() {
            return GetAll(() => repo.GetAllPeople());
        }

        public void Remove(Person person) {
            Cache.Remove(person);
        }

        public void Remove(IEnumerable<Person> peopleToRemove) {
            Cache.Remove(peopleToRemove);
        }
    }
}