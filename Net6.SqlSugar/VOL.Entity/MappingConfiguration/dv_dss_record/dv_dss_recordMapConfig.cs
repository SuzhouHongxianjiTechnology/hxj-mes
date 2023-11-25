using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class dv_dss_recordMapConfig : EntityMappingConfiguration<dv_dss_record>
    {
        public override void Map(EntityTypeBuilder<dv_dss_record>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

