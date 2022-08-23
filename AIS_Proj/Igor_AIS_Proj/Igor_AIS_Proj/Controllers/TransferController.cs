
namespace Igor_AIS_Proj.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferBusiness _transferBusiness;
        private readonly ILogger<TransferController> _logger;
        public TransferController(ITransferBusiness transferBusiness, ILogger<TransferController> logger)
        {
            _transferBusiness = transferBusiness;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<Transfer> GetById(int id) => await _transferBusiness.GetById(id);

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id) => await _transferBusiness.Delete(id);

        [HttpGet]
        public List<Transfer> GetAll() => _transferBusiness.GetAll();

        [HttpPut]
        public Task<bool> Update(Transfer transfer) => _transferBusiness.Update(transfer);

        [HttpPost]
        public async Task<Transfer> Create(Transfer transfer) => await _transferBusiness.Create(transfer);


        [HttpGet("{id}")]
        public void GettAllTransfersUser(int id)
        {

            _transferBusiness.GetAllTransfersUser(id);

        }

    }
}
