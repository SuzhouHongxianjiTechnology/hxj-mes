using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class dv_machinery_typeMapConfig : EntityMappingConfiguration<dv_machinery_type>
    {
        public override void Map(EntityTypeBuilder<dv_machinery_type>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

