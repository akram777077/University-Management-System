using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories
{
    public class StudentRepoTests
    {
        private const int ValidStudentId = 1;
        private const string ValidLastName = "Smith";

        #region Get All Students
        [Fact]
        public async Task GetAllAsync_WhenStudentsExist_ReturnsListOfStudents()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().HaveCount(1);
            result.Should().ContainEquivalentOf(student);
        }

        [Fact]
        public async Task GetAllAsync_WhenLargeDataSet_ReturnsAllStudents()
        {
            // Arrange
            var students = Enumerable.Range(1, 500).Select(i => new Student
            {
                Id = i,
                FName = $"FirstName{i}",
                LName = $"LastName{i}",
            }).ToList();

            using var context = CreateInMemoryDbContext();
            context.Students.AddRange(students);
            await context.SaveChangesAsync();

            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().HaveCount(500);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoStudentsExist_ReturnsEmptyList()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }
        #endregion

        #region Add Student
        [Fact]
        public async Task AddAsync_WhenStudentIsValid_ReturnsStudentId()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);
            var student = CreateTestStudent();

            // Act
            var result = await repo.AddAsync(student);

            // Assert
            result.Should().Be(ValidStudentId);
            context.Students.Should().ContainEquivalentOf(student);
        }

        [Fact]
        public async Task AddAsync_WhenStudentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.AddAsync(null!));
        }
        #endregion

        #region Delete Student
        [Fact]
        public async Task DeleteAsync_ById_WhenStudentExists_ReturnsTrue()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DeleteAsync(ValidStudentId);

            // Assert
            result.Should().BeTrue();
            context.Students.Should().NotContain(student);
        }

        [Fact]
        public async Task DeleteAsync_ById_WhenStudentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DeleteAsync(999);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_ByLastName_WhenStudentExistsUsingLastName_ReturnsTrue()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DeleteAsync(ValidLastName);

            // Assert
            result.Should().BeTrue();
            context.Students.Should().NotContain(student);
        }

        [Fact]
        public async Task DeleteAsync_ByLastName_WhenStudentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DeleteAsync("NonExistingLastName");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_WhenLastNameIsNullOrEmpty_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DeleteAsync("");

            // Assert
            result.Should().BeFalse();
        }
        #endregion

        #region Get Student
        [Fact]
        public async Task GetByIdAsync_WhenStudentExists_ReturnsStudent()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetByIdAsync(ValidStudentId);

            // Assert
            result.Should().BeEquivalentTo(student);
        }

        [Fact]
        public async Task GetByIdAsync_WhenStudentDoesNotExist_ReturnsNull()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByNameAsync_WhenStudentExists_ReturnsStudent()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetByNameAsync(ValidLastName);

            // Assert
            result.Should().BeEquivalentTo(student);
        }

        [Fact]
        public async Task GetByNameAsync_WhenStudentDoesNotExist_ReturnsNull()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetByNameAsync("NonExistingLastName");

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByNameAsync_WhenLastNameIsNullOrEmpty_ReturnsNull()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.GetByNameAsync("");

            // Assert
            result.Should().BeNull();
        }
        #endregion

        #region Update Student
        [Fact]
        public async Task UpdateAsync_WhenStudentIsValid_ReturnsTrue()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            student.FName = "UpdatedFirstName";

            // Act
            var result = await repo.UpdateAsync(student);

            // Assert
            result.Should().BeTrue();
            var updatedStudent = await repo.GetByIdAsync(ValidStudentId);
            updatedStudent!.FName.Should().Be("UpdatedFirstName");
        }

        [Fact]
        public async Task UpdateAsync_WhenStudentIsNull_ThrowsNullReferenceException()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => repo.UpdateAsync(null!));
        }
        #endregion

        #region Existence Checks
        [Fact]
        public async Task DoesExistAsync_ById_WhenStudentExists_ReturnsTrue()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DoesExistAsync(ValidStudentId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DoesExistAsync_ById_WhenStudentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DoesExistAsync(999);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DoesExistAsync_ByLastName_WhenStudentExists_ReturnsTrue()
        {
            // Arrange
            var student = CreateTestStudent();
            using var context = CreateInMemoryDbContext(student);
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DoesExistAsync(ValidLastName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DoesExistAsync_ByLastName_WhenStudentDoesNotExist_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DoesExistAsync("NonExistingLastName");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DoesExistAsync_ByLastName_WhenLastNameIsNullOrEmpty_ReturnsFalse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new StudentRepository(context);

            // Act
            var result = await repo.DoesExistAsync("");
            
            // Assert
            result.Should().BeFalse();
        }
        #endregion

        #region Private Helpers
        private AppDbContext CreateInMemoryDbContext(Student student = null!)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new AppDbContext(options);

            if (student != null)
            {
                dbContext.Students.Add(student);
                dbContext.SaveChanges();
            }

            return dbContext;
        }

        private Student CreateTestStudent() => new Student
        {
            Id = ValidStudentId,
            FName = "John",
            LName = ValidLastName
        };
        #endregion
    }
}