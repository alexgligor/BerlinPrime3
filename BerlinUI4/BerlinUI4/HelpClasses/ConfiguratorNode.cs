namespace BerlinUI4.HelpClasses
{
    public class ConfiguratorNode<T>
    {
        public T Item { get; set; }
        public bool isOpen = false;
        public ConfiguratorNode(T item)
        {
            Item = item;
        }
    }
}
