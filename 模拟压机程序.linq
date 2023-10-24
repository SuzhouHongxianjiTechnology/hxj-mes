<Query Kind="Program">
  <NuGetReference>HslCommunication</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>HslCommunication.Profinet.Siemens</Namespace>
</Query>

async Task Main()
{
	if (!HslCommunication.Authorization.SetAuthorizationCode("66634f11-68dc-45d5-9638-fccb9f6b8fed"))
	{
		"active failed".Dump();
	}

	SiemensS7Net plc = new SiemensS7Net(SiemensPLCS.S1500, "127.0.0.1");  // 使用了本机的虚拟PLC
	plc.SetPersistentConnection();
	
	await plc.WriteAsync("DB50.1200.4",true);
	
	for (int i = 0; i < 49; i++)
	{
		// 随机写 float
		Random random = new Random();
		float randomNumber = (float)random.NextDouble() + 1.0f;
		await plc.WriteAsync("DB50.1210.0",randomNumber);
		await Task.Delay(10);
	}
	
	await plc.WriteAsync("DB50.1200.4",false);
}

// You can define other methods, fields, classes and namespaces here
// You cannot have no primary key and no conditions