using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class tm_tool_typeMapConfig : EntityMappingConfiguration<tm_tool_type>
    {
        public override void Map(EntityTypeBuilder<tm_tool_type>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

