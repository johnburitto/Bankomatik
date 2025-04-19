using Bankomatik.Actions.Actions;
using Bankomatik.Pipeline.Implementations;

var pipelineService = new PipelineService();
var pipeline = await pipelineService.CreatePipelineAsync();

await pipeline.AddAction<DetectUSBDrivesAction>()
	.AddAction<ChooseDriveToActWithAction>()
	.AddSubAction<ClearConsoleAction>()
	.AddAction<ChooseActionOnDriveAction>()
	.AddSubAction<CreateBankCardAction>()
	.AddSubAction<WithdrawMoneyAction>()
	.AddSubAction<DeleteBankCardAction>()
	.AddSubAction<RepairBankCardAction>()
	.AddAction<ClearConsoleAction>()
	.StartAsync();
