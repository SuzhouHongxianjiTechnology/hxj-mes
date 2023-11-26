using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class bs_coderuleMapConfig : EntityMappingConfiguration<bs_coderule>
    {
        public override void Map(EntityTypeBuilder<bs_coderule>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

