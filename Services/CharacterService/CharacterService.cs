using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API_YU.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {   
        private static List<Character> characters  = new List<Character> {
            //new Character(),
            //new Character{Id = 1, Name = "Luis"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            //character.Id = characters.Max(p => p.Id) + 1;

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            //characters.Add(character);
            ServiceResponse.Data = characters.Select(p=> _mapper.Map<GetCharacterDto>(p)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = characters.FirstOrDefault(p=> p.Id == id);
                if(character is null)
                    throw new Exception($"Character with Id '{id}' not found.");
                characters.Remove(character);
            serviceResponse.Data = characters.Select(p=> _mapper.Map<GetCharacterDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            ServiceResponse.Data = dbCharacters.Select(p=> _mapper.Map<GetCharacterDto>(p)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(p=> p.Id == id);
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
            
            var character = characters.FirstOrDefault(p=> p.Id == updateCharacter.Id);
            if(character is null)
                throw new Exception($"Character with Id '{updateCharacter.Id}' not found.");

                _mapper.Map(updateCharacter, character);


            character.Name = updateCharacter.Name;
            character.Hitpoints = updateCharacter.Hitpoints;
            character.Strength = updateCharacter.Strength;
            character.Defense = updateCharacter.Defense;
            character.Intelligence = updateCharacter.Intelligence;
            character.Class = updateCharacter.Class;
            
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
    }
    }
}