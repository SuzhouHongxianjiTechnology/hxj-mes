using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class tm_tool_returnMapConfig : EntityMappingConfiguration<tm_tool_return>
    {
        public override void Map(EntityTypeBuilder<tm_tool_return>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

