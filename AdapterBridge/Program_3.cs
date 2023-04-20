namespace AdapterBridge
{
    public class Program_3 : IProgramRunner
    {
        private readonly IProgramRunner m_program_1;
        private readonly IProgramRunner m_program_2;
        public Program_3(IProgramRunner program_1, IProgramRunner program_2)
        {
            m_program_1 = program_1;
            m_program_2 = program_2;
        }

        public void Run(IReadWriteCreator creator)
        {
            creator = new ProgramAdapter(creator);

            m_program_2.Run(creator);
            m_program_1.Run(creator); 
        }
    }
}
