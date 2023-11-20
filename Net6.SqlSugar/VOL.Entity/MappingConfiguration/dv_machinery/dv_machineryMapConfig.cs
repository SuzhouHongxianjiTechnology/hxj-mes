using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class dv_machineryMapConfig : EntityMappingConfiguration<dv_machinery>
    {
        public override void Map(EntityTypeBuilder<dv_machinery>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

