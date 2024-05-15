using MambaASPNet.Business.Exceptions;
using MambaASPNet.Business.Services.Abstracts;
using MambaASPNet.Core.Models;
using MambaASPNet.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamService(ITeamRepository teamRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            _teamRepository = teamRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreateTeam(Team team)
        {
            if (team.ImageFile == null) throw new FileNullReferenceException("ImageFile","File Null Reference Error");
            if (!team.ImageFile.ContentType.Contains("image/")) 
                throw new FileContentTypeException("ImageFile","File content type error!");

            if (team.ImageFile.Length > 2097152) 
                throw new FileSizeException("ImageFile","File Size Error");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(team.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\teams\" + fileName;
            
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                team.ImageFile.CopyTo(stream);
            }

            team.ImageUrl = fileName;
            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void DeleteTeam(int id)
        {
            var existTeam = _teamRepository.Get(x=>x.Id==id);
            if (existTeam == null) throw new EntityNotFoundException("", "Team not Found");

            string path = _webHostEnvironment.WebRootPath + @"\uploads\teams\" + existTeam.ImageUrl;

            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("ImageFile","File not found!");

            File.Delete(path);

            _teamRepository.Delete(existTeam);
            _teamRepository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void UpdateTeam(int id, Team team)
        {
            var olderTeam = _teamRepository.Get(x => x.Id == id);
            if (olderTeam == null) throw new EntityNotFoundException("", "Team Not found!");

            if(team.ImageFile != null)
            {
                if (!team.ImageFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type error!");

                if (team.ImageFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "File Size Error");

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(team.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\teams\" + fileName;

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    team.ImageFile.CopyTo(stream);
                }

                olderTeam.ImageUrl = fileName;
            }

            olderTeam.Name = team.Name;
            olderTeam.Surname = team.Surname;
            olderTeam.Position = team.Position;

            _teamRepository.Commit();
        }
    }
}
